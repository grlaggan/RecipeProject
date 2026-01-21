using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeProject.Domain.Entities.ProductEntity;
using RecipeProject.Domain.Entities.RecipeEntity;

namespace RecipeProject.Infrastructure.Configurations;

public class RecipeProductConfiguration : IEntityTypeConfiguration<RecipeProduct>
{
    public void Configure(EntityTypeBuilder<RecipeProduct> builder)
    {
        
        builder.ToTable("recipes_products");
        
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        
        builder.HasOne<Recipe>()
            .WithMany(r => r.Products)
            .HasForeignKey(p => p.RecipeId);

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(p => p.ProductId);
    }
}