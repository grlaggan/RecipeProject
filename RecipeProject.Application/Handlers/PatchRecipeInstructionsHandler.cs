using FluentResults;
using MediatR;
using RecipeProject.Application.Abstracts;
using RecipeProject.Application.Commands;

namespace RecipeProject.Application.Handlers;

public class PatchRecipeInstructionsHandler(IRecipesRepository repository)
    : IRequestHandler<PatchRecipeInstructionCommand, Result>
{

    public Task<Result> Handle(PatchRecipeInstructionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}