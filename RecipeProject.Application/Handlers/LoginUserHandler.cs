using FluentResults;
using MediatR;
using RecipeProject.Application.Abstracts;
using RecipeProject.Application.Commands;

namespace RecipeProject.Application.Handlers;

public class LoginUserHandler(
    IJwtProvider provider,
    IUsersRepository repository,
    IPasswordHasher passwordHasher) : IRequestHandler<LoginUserCommand, Result<string>>
{

    public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.GetByEmailAsync(request.Email, cancellationToken);

        if (result.IsFailed)
            return Result.Fail("Something is wrong. Try again");

        var passwordCheckingResult = passwordHasher.Verify(request.password, result.Value.PasswordHash);
        
        if (passwordCheckingResult.IsFailed)
            return Result.Fail(passwordCheckingResult.Errors[0]);

        var token = provider.Generate(result.Value);

        return token;
    }
}