using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeProject.Domain.Entities.ProductEntity;
using RecipeProject.Infrastructure.Utils;

namespace RecipeProject.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(p => p.Id).HasName("pk_products");
        builder.Property(p => p.Id).HasColumnName("id");
        
        builder.Property(p => p.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(Constants.Length128);
        
        builder.Property(p => p.Description)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(Constants.Length1024);

        builder.Property(p => p.Type)
            .IsRequired();
    }
}