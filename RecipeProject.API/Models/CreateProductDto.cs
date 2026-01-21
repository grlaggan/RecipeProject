using RecipeProject.Domain.Entities.ProductEntity;

namespace RecipeProject.API.Models;

public record CreateProductDto(string Name, string Description, ProductType Type);