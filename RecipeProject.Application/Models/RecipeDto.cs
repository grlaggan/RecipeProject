using RecipeProject.Domain.Entities.ProductEntity;

namespace RecipeProject.Application.Models;

public record RecipeDto(Guid Id, string Name, string Description, string Instructions);