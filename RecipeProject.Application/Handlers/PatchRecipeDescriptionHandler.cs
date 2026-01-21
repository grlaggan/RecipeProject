using FluentResults;
using MediatR;
using RecipeProject.Application.Abstracts;
using RecipeProject.Application.Commands;

namespace RecipeProject.Application.Handlers;

public class PatchRecipeDescriptionHandler(IRecipesRepository repository)
    : IRequestHandler<PatchRecipeDescriptionCommand, Result>
{

    public async Task<Result> Handle(PatchRecipeDescriptionCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.PatchDescription(request.Id, request.Description, cancellationToken);
        
        if (result.IsFailed)
            return Result.Fail(result.Errors[0]);
        
        return Result.Ok();
    }
}