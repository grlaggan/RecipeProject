using FluentResults;
using RecipeProject.Domain.Entities.ProductEntity;

namespace RecipeProject.Domain.Entities.RecipeEntity;

public class RecipeProduct : BaseEntity
{
    public RecipeProduct(Guid id, Guid recipeId, Guid productId)
    {
        Id = id;
        RecipeId = recipeId;
        ProductId = productId;
    }

    public Guid RecipeId { get; set; }
    public Guid ProductId { get; set; }

    public static Result<RecipeProduct> Create(Guid recipeId, Guid productId)
    {
        return new RecipeProduct(Guid.NewGuid(), recipeId, productId);
    }
}