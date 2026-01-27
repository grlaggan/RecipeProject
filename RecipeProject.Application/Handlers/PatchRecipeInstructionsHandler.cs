using FluentResults;
using MediatR;
using RecipeProject.Application.Abstracts;
using RecipeProject.Application.Commands;

namespace RecipeProject.Application.Handlers;

public class PatchRecipeInstructionsHandler(IRecipesRepository repository)
    : IRequestHandler<PatchRecipeInstructionCommand, Result>
{

    public async Task<Result> Handle(PatchRecipeInstructionCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.PatchInstructions(request.Id, request.Instructions, cancellationToken);

        if (result.IsFailed)
            return Result.Fail(result.Errors[0]);

        return Result.Ok();
    }
}