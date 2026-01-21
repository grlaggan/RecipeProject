using FluentResults;
using MediatR;

namespace RecipeProject.Application.Commands;

public record CreateRecipeCommand(string Name, string Description, string Instructions, 
    List<Guid> ProductIds) : IRequest<Result<Guid>>;