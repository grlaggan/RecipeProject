using Microsoft.EntityFrameworkCore;
using RecipeProject.Domain.Entities.ProductEntity;
using RecipeProject.Domain.Entities.RecipeEntity;

namespace RecipeProject.Application.Abstracts;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; set; }
    DbSet<Recipe> Recipes { get; set; } 
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}