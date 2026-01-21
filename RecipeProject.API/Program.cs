using Microsoft.EntityFrameworkCore;
using RecipeProject.Application.Abstracts;
using RecipeProject.Application.Handlers;
using RecipeProject.Application.ModelProfiles;
using RecipeProject.Infrastructure;
using RecipeProject.Infrastructure.Repositories;
using RecipeProject.Infrastructure.Transactions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddScoped<ITransactionManager, TransactionManager>();
builder.Services.AddRepositories();
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly((typeof(CreateProductHandler).Assembly)));
builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ProductProfile>();
    cfg.AddProfile<RecipeProfile>();
});

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger()
        .UseSwaggerUI(opt =>
        {
            opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
}


app.Run();
