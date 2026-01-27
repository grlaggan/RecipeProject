using Microsoft.Extensions.DependencyInjection;
using RecipeProject.Application.Abstracts;

namespace RecipeProject.Infrastructure.Repositories;

public static class RepositoriesDi
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddRepositories()
        {
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IRecipesRepository, RecipesRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            
            return services;
        }
    }
}