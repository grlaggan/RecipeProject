using FluentResults;
using MediatR;
using RecipeProject.Application.Models;

namespace RecipeProject.Application.Queries;

public record GetRecipesQuery() : IRequest<Result<List<RecipeDto>>>;
