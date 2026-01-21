using FluentResults;
using MediatR;
using RecipeProject.Application.Models;

namespace RecipeProject.Application.Queries;

public record GetRecipesByProductIdQuery(Guid ProductId) : IRequest<Result<List<RecipeDto>>>;
