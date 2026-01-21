using Microsoft.EntityFrameworkCore;
using RecipeProject.Application.Abstracts;
using RecipeProject.Domain.Entities.ProductEntity;
using RecipeProject.Domain.Entities.RecipeEntity;

namespace RecipeProject.Infrastructure;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public ApplicationDbContext()
    {
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Recipe> Recipes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}