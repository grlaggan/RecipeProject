using RecipeProject.Domain.Entities.ProductEntity;

namespace RecipeProject.Application.Models;

public record CreateProductRequest(string Name, string Description, ProductType Type);