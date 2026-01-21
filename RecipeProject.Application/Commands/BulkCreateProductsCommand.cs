using FluentResults;
using MediatR;
using RecipeProject.Application.Models;

namespace RecipeProject.Application.Commands;

public record BulkCreateProductsCommand(List<CreateProductRequest> Products) : IRequest<Result<int>>;
    
