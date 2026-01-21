namespace RecipeProject.Application.Models;

public record BulkCreateProductsRequest(List<CreateProductRequest> Products);