using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeProject.Application.Abstracts;

namespace RecipeProject.Infrastructure;

public static class ApplicationDbContextDi
{
    private const string PostgresConnection = "DefaultConnection";
    
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplicationDbContext(IConfiguration configuration)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(opt =>
            {   
                opt.UseNpgsql(configuration.GetConnectionString(PostgresConnection));
            });

            return services;
        }
    }
}