using FluentResults;

namespace RecipeProject.Domain.Entities.UserEntity;

public class User : BaseEntity
{
    public Guid Id { get; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public List<Role> Roles { get; set; } = [];

    public User(Guid id, string username, string passwordHash, string email)
    {
        Id = id;
        Username = username;
        PasswordHash = passwordHash;
        Email = email;
    }

    public static Result<User> Create(string username, string passwordHash, string email, List<Role> roles)
    {
        if (string.IsNullOrEmpty(username))
            return Result.Fail("username cannot be empty");

        if (string.IsNullOrEmpty(passwordHash))
            return Result.Fail("password cannot be empty");
        
        if (string.IsNullOrEmpty(email))
            return Result.Fail("email cannot be empty");

        var user = new User(Guid.NewGuid(), username, passwordHash, email);

        user.AddRoles(roles);
        
        return user;
    }

    private void AddRoles(List<Role> roles)
    {
        Roles.AddRange(roles);
    }
}