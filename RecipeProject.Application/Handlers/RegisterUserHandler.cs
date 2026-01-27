using FluentResults;
using MediatR;
using RecipeProject.Application.Abstracts;
using RecipeProject.Application.Commands;

namespace RecipeProject.Application.Handlers;

public class RegisterUserHandler(
    IUsersRepository repository) : IRequestHandler<RegisterUserCommand, Result<Guid>>
{

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var rolesResult = await repository.GetRolesByEnums(request.roles, cancellationToken);

        if (rolesResult.IsFailed)
            return Result.Fail(rolesResult.Errors[0]);
        
        var result = await repository.CreateAsync(request.username, request.password,
            request.email, rolesResult.Value, cancellationToken);

        if (result.IsFailed)
            return Result.Fail(result.Errors[0]);

        return result.Value;
    }
}