using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace RecipeProject.Application.Authentication;

public class PermissionAuthorizationHandler(IServiceScopeFactory scopeFactory)
    : AuthorizationHandler<PermissionRequirement>
{

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var claim = context.User.Claims.FirstOrDefault(c =>
            c.Type == ClaimTypes.NameIdentifier);

        if (claim is null || !Guid.TryParse(claim.Value, out var id))
        {
            return;
        }
        
        using var scope = scopeFactory.CreateScope();

        var permissionService = scope.ServiceProvider.GetRequiredService<PermissionService>();

        var result = await permissionService.GetPermissionAsync(id, CancellationToken.None);
        
        if (result.Value.Intersect(requirement.Permissions).Any())
            context.Succeed(requirement);
        
    }
}