using Microsoft.AspNetCore.Authorization;
using RecipeProject.Domain.Enums;

namespace RecipeProject.Application.Authentication;

public class PermissionRequirement(List<PermissionEnum> permissions) : IAuthorizationRequirement
{
    public List<PermissionEnum> Permissions { get; } = permissions;
} 