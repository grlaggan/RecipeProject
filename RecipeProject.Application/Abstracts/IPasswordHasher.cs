using FluentResults;

namespace RecipeProject.Application.Abstracts;

public interface IPasswordHasher
{
    string Hash(string password);
    Result Verify(string password, string hashedPassword);
}