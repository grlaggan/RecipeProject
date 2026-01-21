using FluentResults;
using MediatR;
using RecipeProject.Application.Abstracts;
using RecipeProject.Application.Commands;

namespace RecipeProject.Application.Handlers;

public class DeleteProductHandler(IProductsRepository repository) : IRequestHandler<DeleteProductCommand, Result>
{
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var resultGet = await repository.GetById(request.Id, cancellationToken);
        
        if (resultGet.IsFailed)
            return Result.Fail(resultGet.Errors[0]);

        var resultDelete = await repository.DeleteAsync(resultGet.Value, cancellationToken);
        
        if (resultDelete.IsFailed)
            return Result.Fail(resultDelete.Errors[0]);
        
        return Result.Ok();
    }
}