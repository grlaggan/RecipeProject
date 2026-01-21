using FluentResults;
using MediatR;

namespace RecipeProject.Application.Commands;

public record PatchRecipeNameCommand(Guid Id, string Name) : IRequest<Result>;