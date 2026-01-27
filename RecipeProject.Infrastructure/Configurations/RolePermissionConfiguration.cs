using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeProject.Domain.Entities.UserEntity;
using RecipeProject.Domain.Enums;
using RecipeProject.Infrastructure.Models;

namespace RecipeProject.Infrastructure.Configurations;

public class RolePermissionConfiguration
    : IEntityTypeConfiguration<RolePermission>
{

    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(rp => new {rp.RoleId, rp.PermissionId});

        builder.HasData(ParseRolePermissions());
    }

    private RolePermission[] ParseRolePermissions()
    {
        return
        [
            new RolePermission{RoleId = 1, PermissionId = 1},
            new RolePermission{RoleId = 1, PermissionId = 2},
            new RolePermission{RoleId = 1, PermissionId = 3},
            new RolePermission{RoleId = 1, PermissionId = 4},
            new RolePermission{RoleId = 2, PermissionId = 2},
        ];
    }
}