using RecipeProject.Domain.Entities.UserEntity;

namespace RecipeProject.Infrastructure.Models;

public class AuthorizationOptions
{
    public List<RolePermissions> RolePermissions { get; set; } = [];
}

public class RolePermissions
{
    public string Role { get; set; } = string.Empty;
    public List<string> Permissions { get; set; } = [];
}