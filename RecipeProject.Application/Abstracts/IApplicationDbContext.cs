using Microsoft.EntityFrameworkCore;
using RecipeProject.Domain.Entities.ProductEntity;
using RecipeProject.Domain.Entities.RecipeEntity;
using RecipeProject.Domain.Entities.UserEntity;

namespace RecipeProject.Application.Abstracts;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; set; }
    DbSet<Recipe> Recipes { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<UserRole> UserRole { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}