namespace RecipeProject.Application.Models;

public record GetProductsResponse(string detail, List<ProductDto> products);