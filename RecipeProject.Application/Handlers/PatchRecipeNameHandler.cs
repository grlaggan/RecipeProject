using FluentResults;
using MediatR;
using RecipeProject.Application.Abstracts;
using RecipeProject.Application.Commands;

namespace RecipeProject.Application.Handlers;

public class PatchRecipeNameHandler(IRecipesRepository repository) : IRequestHandler<PatchRecipeNameCommand, Result>
{
    public async Task<Result> Handle(PatchRecipeNameCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.PatchName(request.Id, request.Name, cancellationToken);
        
        return result.IsFailed ? Result.Fail(result.Errors[0]) : Result.Ok();
    }
}