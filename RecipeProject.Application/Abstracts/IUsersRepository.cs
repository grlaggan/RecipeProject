using FluentResults;
using RecipeProject.Domain.Entities.UserEntity;
using RecipeProject.Domain.Enums;

namespace RecipeProject.Application.Abstracts;

public interface IUsersRepository
{
    Task<Result<Guid>> CreateAsync(string username, string password, string email, List<Role> roles,
        CancellationToken cancellationToken);

    Task<Result<User>> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<Result<HashSet<PermissionEnum>>> GetUserPermissionsAsync(Guid userId, CancellationToken cancellationToken);
    Task<Result<List<Role>>> GetRolesByEnums(List<RoleEnum> roles, CancellationToken cancellationToken);
}