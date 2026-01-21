using FluentResults;
using MediatR;
using RecipeProject.Application.Models;

namespace RecipeProject.Application.Queries;

public record GetProductsQuery() : IRequest<Result<List<ProductDto>>>;