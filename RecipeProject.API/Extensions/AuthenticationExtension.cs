using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RecipeProject.Application.Authentication;
using RecipeProject.Domain.Enums;
using RecipeProject.Infrastructure.Models;

namespace RecipeProject.API.Extensions;

public static class AuthenticationExtension
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApiAuthentication(IConfiguration configuration)
        {
            var options = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>()!;
            
            if (string.IsNullOrWhiteSpace(options.Token))
                throw new InvalidOperationException("Token is not configured. Check Vault");
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = options.Issuer,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(options.Token)),
                        ValidateLifetime = true,
                    };

                    opt.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["access_token"];

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(Policies.Create, policy => 
                    policy.AddRequirements(new PermissionRequirement([PermissionEnum.Create])));
                opt.AddPolicy(Policies.Read, policy => 
                    policy.AddRequirements(new PermissionRequirement([PermissionEnum.Read])));
            });
            
            return services;
        }
    }
}