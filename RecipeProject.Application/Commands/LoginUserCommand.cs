using FluentResults;
using MediatR;

namespace RecipeProject.Application.Commands;

public record LoginUserCommand(string Email, string password) : IRequest<Result<string>>;