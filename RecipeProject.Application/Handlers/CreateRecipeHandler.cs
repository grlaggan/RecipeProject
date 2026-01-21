using FluentResults;
using MediatR;
using RecipeProject.Application.Abstracts;
using RecipeProject.Application.Commands;
using RecipeProject.Domain.Entities.ProductEntity;
using RecipeProject.Domain.Entities.RecipeEntity;

namespace RecipeProject.Application.Handlers;

public class CreateRecipeHandler(IRecipesRepository repository, IProductsRepository productsRepository)
    : IRequestHandler<CreateRecipeCommand, Result<Guid>>
{

    public async Task<Result<Guid>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        List<Product> products = [];
        foreach (var productId in request.ProductIds)
        {
            var result = await productsRepository.GetById(productId, cancellationToken);

            if (result.IsFailed)
                return Result.Fail(result.Errors[0]);

            products.Add(result.Value);
        }

        var resultCreationRecipe = Recipe.Create(request.Name, request.Description, request.Instructions, products);

        if (resultCreationRecipe.IsFailed)
            return Result.Fail(resultCreationRecipe.Errors[0]);

        var resultRecipeToDb = await repository.Create(resultCreationRecipe.Value, cancellationToken);
        
        if (resultRecipeToDb.IsFailed)
            return Result.Fail(resultRecipeToDb.Errors[0]);
        
        return resultRecipeToDb.Value;
    }
}