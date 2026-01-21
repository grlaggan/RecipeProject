using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeProject.Domain.Entities.RecipeEntity;
using RecipeProject.Infrastructure.Utils;

namespace RecipeProject.Infrastructure.Configurations;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.HasKey(r => r.Id).HasName("pk_recipes");
        builder.Property(r => r.Id).HasColumnName("id");
        
        builder.Property(r => r.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(Constants.Length128);
        
        builder.Property(r => r.Description)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(Constants.Length1024);
        
        builder.Property(r => r.Instructions)
            .HasColumnName("instructions")
            .IsRequired()
            .HasMaxLength(Constants.Length1024);
        
        builder.ToTable("recipes");
    }
}