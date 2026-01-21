using FluentResults;
using MediatR;
using RecipeProject.Application.Abstracts;
using RecipeProject.Application.Commands;
using RecipeProject.Domain.Entities.ProductEntity;

namespace RecipeProject.Application.Handlers;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    private readonly IProductsRepository _productsRepository;

    public CreateProductHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }
    
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var result = Product.Create(request.Name, request.Description, request.Type);

        if (result.IsFailed)
            return Result.Fail(result.Errors[0]);

        var newProduct = result.Value;

        var creationResult = await _productsRepository.Create(newProduct, cancellationToken);
        
        if (creationResult.IsFailed)
            return Result.Fail(creationResult.Errors[0]);
        
        return creationResult.Value;
    }
}