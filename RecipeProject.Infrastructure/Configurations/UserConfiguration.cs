using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeProject.Domain.Entities.UserEntity;
using RecipeProject.Infrastructure.Utils;

namespace RecipeProject.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id).HasName("pk_users");
        builder.Property(u => u.Id).HasColumnName("id");
        
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(Constants.Length128)
            .HasColumnName("username");

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasColumnName("password");
        
        builder.Property(u => u.Email)
            .IsRequired()
            .HasColumnName("email");

        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<UserRole>(
                l => l.HasOne<Role>().WithMany()
                    .HasForeignKey(r => r.RoleId),
                r => r.HasOne<User>().WithMany()
                    .HasForeignKey(u => u.UserId)
            );
    }
}