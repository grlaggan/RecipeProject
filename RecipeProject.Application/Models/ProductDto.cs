using RecipeProject.Domain.Entities.ProductEntity;

namespace RecipeProject.Application.Models;

public record ProductDto(Guid Id, string Name, string Description, ProductType Type);