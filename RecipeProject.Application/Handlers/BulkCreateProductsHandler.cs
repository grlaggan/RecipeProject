using FluentResults;
using MediatR;
using RecipeProject.Application.Abstracts;
using RecipeProject.Application.Commands;
using RecipeProject.Domain.Entities.ProductEntity;

namespace RecipeProject.Application.Handlers;

public class BulkCreateProductsHandler : IRequestHandler<BulkCreateProductsCommand, Result<int>>
{
    private readonly IProductsRepository _repository;
    private readonly ITransactionManager _transactionManager;

    public BulkCreateProductsHandler(IProductsRepository repository, ITransactionManager transactionManager)
    {
        _repository = repository;
        _transactionManager = transactionManager;
    }
    
    public async Task<Result<int>> Handle(BulkCreateProductsCommand request, CancellationToken cancellationToken)
    {
        var resultBeginTransaction = await _transactionManager.BeginTransaction(cancellationToken);
        
        if (resultBeginTransaction.IsFailed)
            return Result.Fail(resultBeginTransaction.Errors[0]);

        using var transactionScope = resultBeginTransaction.Value;

        var count = 0;
        foreach (var result in request.Products.Select(productDto => 
                     Product.Create(productDto.Name, productDto.Description, productDto.Type)))
        {
            if (result.IsFailed)
            {
                transactionScope.Rollback();
                return Result.Fail(result.Errors[0]);
            }

            var resultCreationProduct = await _repository.Create(result.Value, cancellationToken);
            
            if (resultCreationProduct.IsFailed)
            {
                transactionScope.Rollback();
                return Result.Fail(resultCreationProduct.Errors[0]);
            }
            
            count++;
        }

        await _transactionManager.SaveChangesAsync(cancellationToken);
        var resultCommit = transactionScope.Commit();
        
        if (resultCommit.IsFailed)
            return Result.Fail(resultCommit.Errors[0]);

        return count;
    }
}