using FluentResults;
using MediatR;
using RecipeProject.Domain.Entities.ProductEntity;

namespace RecipeProject.Application.Commands;

public record CreateProductCommand(string Name, string Description, ProductType Type) : IRequest<Result<Guid>>;