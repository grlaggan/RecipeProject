using FluentResults;
using RecipeProject.Application.Abstracts;

namespace RecipeProject.Infrastructure.Utils;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public Result Verify(string password, string hashedPassword)
    {
        var isValid = BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        return isValid ? Result.Ok() : Result.Fail("Something is wrong. Try again");
    }
}