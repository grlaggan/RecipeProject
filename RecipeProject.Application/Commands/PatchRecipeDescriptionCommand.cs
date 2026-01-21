using FluentResults;
using MediatR;

namespace RecipeProject.Application.Commands;

public record PatchRecipeDescriptionCommand(Guid Id, string Description) : IRequest<Result>;