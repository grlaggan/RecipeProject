using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecipeProject.API.Models;
using RecipeProject.Application.Commands;

namespace RecipeProject.API.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController(IMediator mediator) : ControllerBase
{

    [HttpPost("register")]
    public async Task<ActionResult<Guid>> Register([FromBody] RegisterRequest request)
    {
        var result = await mediator.Send(new RegisterUserCommand(
            request.Username,
            request.Password,
            request.Email,
            request.roles
        ));

        if (result.IsFailed)
            return Problem(
                    title: "Could not register",
                    detail: result.Errors[0].Message,
                    statusCode: StatusCodes.Status400BadRequest
                );

        return Ok(result.Value);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var result = await mediator.Send(new LoginUserCommand(
            request.Email,
            request.Password
        ));
        
        if (result.IsFailed)
            return Problem(
                title: "Could not login",
                detail: result.Errors[0].Message,
                statusCode: StatusCodes.Status400BadRequest
            ); 
        
        HttpContext.Response.Cookies.Append("access_token", result.Value);
        
        return Ok();
    }
}
