using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RecipeProject.API.Extensions;
using RecipeProject.Application.Abstracts;
using RecipeProject.Application.Authentication;
using RecipeProject.Application.Handlers;
using RecipeProject.Application.ModelProfiles;
using RecipeProject.Infrastructure;
using RecipeProject.Infrastructure.Models;
using RecipeProject.Infrastructure.Repositories;
using RecipeProject.Infrastructure.Transactions;
using RecipeProject.Infrastructure.Utils;
using Scalar.AspNetCore;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;
using AuthorizationOptions = RecipeProject.Infrastructure.Models.AuthorizationOptions;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    await LoadVaultConfiguration();
}

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddOptions<JwtOptions>()
    .Bind(builder.Configuration.GetSection(nameof(JwtOptions)))
    .Validate(o => !string.IsNullOrWhiteSpace(o.Token), "Token is required")
    .ValidateOnStart();

builder.Services.Configure<AuthorizationOptions>(builder.Configuration.GetSection(nameof(AuthorizationOptions)));

builder.WebHost.ConfigureKestrel(opt => 
    {
       opt.ListenAnyIP(8080);
    });

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddScoped<ITransactionManager, TransactionManager>();
builder.Services.AddRepositories();
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly((typeof(CreateProductHandler).Assembly)));
builder.Services.AddControllers();
builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ProductProfile>();
    cfg.AddProfile<RecipeProfile>();
});

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<PermissionService>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

app.MapControllers();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwagger()
        .UseSwaggerUI(opt =>
        {
            opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();

async Task LoadVaultConfiguration()
{
    var authMethod = new TokenAuthMethodInfo("root"); 
    var vaultClient = new VaultClient(
            new VaultClientSettings("http://vault:8200", authMethod));
    
    var secrets = await vaultClient.V1.Secrets.KeyValue.V2
            .ReadSecretAsync(path: "recipe-api", mountPoint: "secret");
    
    foreach (var kv in secrets.Data.Data) 
    {
            builder.Configuration[kv.Key] = kv.Value?.ToString();
    }
}

