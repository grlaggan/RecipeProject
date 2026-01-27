using FluentResults;
using RecipeProject.Application.Abstracts;
using RecipeProject.Domain.Enums;

namespace RecipeProject.Application.Authentication;

public class PermissionService(IUsersRepository usersRepository)
{
    public async Task<Result<HashSet<PermissionEnum>>> GetPermissionAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await usersRepository.GetUserPermissionsAsync(userId, cancellationToken);
    }
}