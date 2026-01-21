namespace RecipeProject.API.Models;

public record CreateRecipeDto(string Name, string Description, string Instructions, List<Guid> ProductsIds);