using FluentResults;
using MediatR;
using RecipeProject.Application.Abstracts;
using RecipeProject.Application.Models;
using RecipeProject.Application.Queries;

namespace RecipeProject.Application.Handlers;

public class GetProductsHandler(IProductsRepository repository)
    : IRequestHandler<GetProductsQuery, Result<List<ProductDto>>>
{

    public async Task<Result<List<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetAllAsync(cancellationToken);

        if (result.IsFailed)
            return Result.Fail(result.Errors[0]);

        var products = result.Value;
        
        return products;
    }
}