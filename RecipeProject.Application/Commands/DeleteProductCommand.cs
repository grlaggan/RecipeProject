using FluentResults;
using MediatR;

namespace RecipeProject.Application.Commands;

public record DeleteProductCommand(Guid Id) : IRequest<Result>;