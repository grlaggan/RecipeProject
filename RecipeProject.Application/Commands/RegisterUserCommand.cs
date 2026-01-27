using FluentResults;
using MediatR;
using RecipeProject.Domain.Entities.UserEntity;
using RecipeProject.Domain.Enums;

namespace RecipeProject.Application.Commands;

public record RegisterUserCommand(string username, string password, string email, List<RoleEnum> roles) : IRequest<Result<Guid>>;