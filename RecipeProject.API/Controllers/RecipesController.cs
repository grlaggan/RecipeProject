using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecipeProject.API.Models;
using RecipeProject.Application.Commands;
using RecipeProject.Application.Models;
using RecipeProject.Application.Queries;

namespace RecipeProject.API.Controllers;

[ApiController]
[Route("/api/recipes")]
public class RecipesController(IMediator mediator) : ControllerBase
{

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateRecipeDto request)
    {
        var result = await mediator.Send(new CreateRecipeCommand(
            request.Name,
            request.Description,
            request.Instructions,
            request.ProductsIds
        ));

        if (result.IsFailed)
            return Problem(
                title: "Could not create recipe",
                detail: result.Errors[0].Message,
                statusCode: StatusCodes.Status400BadRequest
            );

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<ActionResult<List<RecipeDto>>> GetRecipes([FromQuery] Guid? productId)
    {
        if (productId != null)
        {
            var resultById = await mediator.Send(new GetRecipesByProductIdQuery(productId ?? 
                throw new ArgumentNullException()));

            if (resultById.IsFailed)
                return Problem(
                    title: "Could not get recipes by product id",
                    detail: resultById.Errors[0].Message,
                    statusCode: StatusCodes.Status404NotFound
                );

            return Ok(resultById.Value);
        }

        var result = await mediator.Send(new GetRecipesQuery());

        if (result.IsFailed)
            return Problem(
                    title: "Could not get recipes",
                    detail: result.Errors[0].Message,
                    statusCode: StatusCodes.Status404NotFound
                );

        return Ok(result.Value);
    }

    [HttpPatch("/name/{id:guid}")]
    public async Task<IActionResult> PatchName([FromRoute] Guid id, [FromBody] PatchRecipeNameRequest request)
    {
        var result = await mediator.Send(new PatchRecipeNameCommand(id, request.Name));
        
        if (result.IsFailed)
            return Problem(
                title: "Could not change recipe's name",
                detail: result.Errors[0].Message,
                statusCode: StatusCodes.Status400BadRequest
            );
        
        return Ok();
    }

    [HttpPost("/description/{id:guid}")]
    public async Task<IActionResult> PatchDescription([FromRoute] Guid id,
        [FromBody] PatchRecipeDescriptionRequest request)
    {
        var result = await mediator.Send(new PatchRecipeDescriptionCommand(id, request.Description));
        
        if (result.IsFailed)
            return Problem(
                title: "Could not change recipe's description",
                detail: result.Errors[0].Message,
                statusCode: StatusCodes.Status400BadRequest
            );

        return Ok();
    }
    
    [HttpPost("/instructions/{id:guid}")]
        public async Task<IActionResult> PatchInstructions([FromRoute] Guid id,
            [FromBody] PatchRecipeInstructionsRequest request)
        {
            var result = await mediator.Send(new PatchRecipeInstructionCommand(id, request.Instructions));
            
            if (result.IsFailed)
                return Problem(
                    title: "Could not change recipe's description",
                    detail: result.Errors[0].Message,
                    statusCode: StatusCodes.Status400BadRequest
                );
    
            return Ok();
        }
}