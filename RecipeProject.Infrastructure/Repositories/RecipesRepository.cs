using FluentResults;
using Microsoft.EntityFrameworkCore;
using RecipeProject.Application.Abstracts;
using RecipeProject.Domain.Entities.RecipeEntity;

namespace RecipeProject.Infrastructure.Repositories;

public class RecipesRepository(IApplicationDbContext context) : IRecipesRepository
{

    public async Task<Result<Guid>> Create(Recipe recipe, CancellationToken cancellationToken)
    {
        var checkRecipe = await context.Recipes.FirstOrDefaultAsync(r => r.Id == recipe.Id, cancellationToken);

        if (checkRecipe != null)
            return Result.Fail("Recipe with the same id already exists");

        await context.Recipes.AddAsync(recipe, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return recipe.Id;
    }

    public async Task<Result<List<Recipe>>> GetRecipesAsync(CancellationToken cancellationToken)
    {
        var recipes = await context.Recipes.ToListAsync(cancellationToken);

        return recipes;
    }

    public Task<Result<List<Recipe>>> GetRecipesByIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        var recipes = context.Recipes.Where(r =>
            r.Products.Where(pr => pr.ProductId == productId)
            .Select(pr => pr.RecipeId).ToList()
            .Contains(r.Id)
        ).ToList();

        if (recipes.Count == 0)
            return Task.FromResult<Result<List<Recipe>>>(Result.Fail("Recipes by product id was not found"));

        return Task.FromResult<Result<List<Recipe>>>(recipes);
    }

    public async Task<Result> PatchName(Guid id, string name, CancellationToken cancellationToken)
    {
        var recipe = await context.Recipes.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        if (recipe == null)
            return Result.Fail("Recipe with this id was not found");

        recipe.Name = name;

        await context.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }

    public async Task<Result> PatchDescription(Guid id, string description, CancellationToken cancellationToken)
    {
        var recipe = await context.Recipes.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        if (recipe == null)
            return Result.Fail("Recipe with this id was not found");

        recipe.Description = description;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }

    public async Task<Result> PatchInstructions(Guid id, string instructions, CancellationToken cancellationToken)
    {
        var recipe = await context.Recipes.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        if (recipe == null)
            return Result.Fail("Recipe with this id was not found");

        recipe.Instructions = instructions;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}