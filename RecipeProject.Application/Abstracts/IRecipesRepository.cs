using FluentResults;
using RecipeProject.Domain.Entities.RecipeEntity;

namespace RecipeProject.Application.Abstracts;

public interface IRecipesRepository
{
    Task<Result<Guid>> Create(Recipe recipe, CancellationToken cancellationToken);
    Task<Result<List<Recipe>>> GetRecipesAsync(CancellationToken cancellationToken);
    Task<Result<List<Recipe>>> GetRecipesByIdAsync(Guid productId, CancellationToken cancellationToken);
    Task<Result> PatchName(Guid id, string name, CancellationToken cancellationToken);
    Task<Result> PatchDescription(Guid id, string description, CancellationToken cancellationToken);
    Task<Result> PatchInstructions(Guid id, string instructions, CancellationToken cancellationToken);
}