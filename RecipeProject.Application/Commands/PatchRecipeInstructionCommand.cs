using FluentResults;
using MediatR;

namespace RecipeProject.Application.Commands;

public record PatchRecipeInstructionCommand(Guid Id, string Instructions) : IRequest<Result>;