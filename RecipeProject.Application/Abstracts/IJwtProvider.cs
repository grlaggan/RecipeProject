using RecipeProject.Domain.Entities.UserEntity;

namespace RecipeProject.Application.Abstracts;

public interface IJwtProvider
{
    string Generate(User user);
}