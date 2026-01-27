using FluentResults;
using Microsoft.EntityFrameworkCore;
using RecipeProject.Application.Abstracts;
using RecipeProject.Domain.Entities.UserEntity;
using RecipeProject.Domain.Enums;

namespace RecipeProject.Infrastructure.Repositories;

public class UsersRepository(IApplicationDbContext context, IPasswordHasher passwordHasher) : IUsersRepository
{

    public async Task<Result<Guid>> CreateAsync(string username, string password, string email, List<Role> roles,
        CancellationToken cancellationToken)
    {
        var checkUser = await context.Users.FirstOrDefaultAsync(
            u => u.Email == email || u.Username == username,
            cancellationToken);

        if (checkUser != null)
            return Result.Fail("this user already exists");

        var result = User.Create(username, passwordHasher.Hash(password), email, roles);

        foreach (var role in roles)
        {
            await context.UserRole.AddAsync(new UserRole
            {
                RoleId = role.Id,
                UserId = result.Value.Id
            }, cancellationToken);
        }
        
        if (result.IsFailed)
            return Result.Fail(result.Errors[0]);
        
        await context.Users.AddAsync(result.Value, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return result.Value.Id;
    }

    public async Task<Result<User>> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        
        if (user == null)
            return Result.Fail("user was not found");

        return user;
    }

    public async Task<Result<HashSet<PermissionEnum>>> GetUserPermissionsAsync(Guid userId, CancellationToken cancellationToken)
    {
        var roles = await context.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Id == userId)
            .Select(u => u.Roles)
            .ToListAsync(cancellationToken);

        return roles
            .SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(p => (PermissionEnum)p.Id)
            .ToHashSet();
    }

    public async Task<Result<List<Role>>> GetRolesByEnums(List<RoleEnum> roles, CancellationToken cancellationToken)
    {
        List<Role> list = [];
        foreach (var roleEnum in roles)
        {
            var role = await context.Roles
                .FirstOrDefaultAsync(r => r.Id == (int)roleEnum, cancellationToken);
            if (role is null)
                return Result.Fail("Role was not found");
            list.Add(role);
        }

        return list;
    }
}