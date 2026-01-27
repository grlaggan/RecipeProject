using Microsoft.EntityFrameworkCore;
using RecipeProject.Application.Abstracts;
using RecipeProject.Domain.Entities.ProductEntity;
using RecipeProject.Domain.Entities.RecipeEntity;
using RecipeProject.Domain.Entities.UserEntity;

namespace RecipeProject.Infrastructure;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    // public ApplicationDbContext()
    // {
    // }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRole { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}