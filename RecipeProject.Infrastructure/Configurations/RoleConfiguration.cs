using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeProject.Domain.Entities.UserEntity;
using RecipeProject.Domain.Enums;
using RecipeProject.Infrastructure.Models;

namespace RecipeProject.Infrastructure.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        
        builder.HasKey(r => r.Id).HasName("pk_roles");
        builder.Property(r => r.Id).HasColumnName("id");
        builder.Property(r => r.Name).HasColumnName("name");

        builder.HasMany(r => r.Permissions)
            .WithMany(p => p.Roles)
            .UsingEntity<RolePermission>(
                    l => l.HasOne<Permission>().WithMany()
                        .HasForeignKey(rb => rb.PermissionId),
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey(rb => rb.RoleId)
                );

        var roles = Enum.GetValues<RoleEnum>()
            .Select(r => new Role{
                Id = (int)r,
                Name = r.ToString()
            });
        
        builder.HasData(roles);
    }
}