using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecipeProject.API.Models;
using RecipeProject.Application.Commands;
using RecipeProject.Application.Models;
using RecipeProject.Application.Queries;

namespace RecipeProject.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateProductDto request)
    {
        var creationResult = await mediator.Send(new CreateProductCommand(
            request.Name,
            request.Description,
            request.Type
        ));
        
        if (creationResult.IsFailed)
            return Problem(
                title: "Could not create product",
                detail: creationResult.Errors[0].Message,
                statusCode: StatusCodes.Status400BadRequest
            );
        
        return Ok(creationResult.Value);
    }

    [HttpGet]
    public async Task<ActionResult<GetProductsResponse>> GetAll()
    {
        var result = await mediator.Send(new GetProductsQuery());
        
        if (result.IsFailed)
            return Problem(
                title: "Could not retrieve products",
                detail: result.Errors[0].Message,
                statusCode: StatusCodes.Status400BadRequest
            );
        
        return Ok(new GetProductsResponse("Products retrieved successfully", result.Value));
    }

    [HttpPost("/bulk")]
    public async Task<ActionResult<int>> CreateBulk([FromBody] BulkCreateProductsRequest request)
    {
        var result = await mediator.Send(new BulkCreateProductsCommand(
            request.Products
        ));
        
        if (result.IsFailed)
            return Problem(
                title: "Could not create products in bulk",
                detail: result.Errors[0].Message,
                statusCode: StatusCodes.Status400BadRequest
            );
        
        return Ok(result.Value);
    }

    [HttpDelete("/{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await mediator.Send(new DeleteProductCommand(id));
        
        if (result.IsFailed)
            return Problem(
                title: "Could not delete product",
                detail: result.Errors[0].Message,
                statusCode: StatusCodes.Status400BadRequest
            );
        
        return NoContent();
    }
}