using FluentResults;
using RecipeProject.Domain.Entities.ProductEntity;

namespace RecipeProject.Domain.Entities.RecipeEntity;

public class Recipe : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Instructions { get; set; }
    public List<RecipeProduct> Products { get; private set; } = [];

    public Recipe(Guid id, string name, string description, string instructions)
    {
        Id = id;
        Name = name;
        Description = description;
        Instructions = instructions;
    }

    private Result AddProducts(List<Product> products)
    {
        var recipeProducts = 
            products.Select(p => RecipeProduct.Create(Id, p.Id).Value).ToList();

        Products.AddRange(recipeProducts);
        return Result.Ok();
    }
    
    public static Result<Recipe> Create(string name, string description, string instructions, List<Product> products)
    {
        var resultValidation = IsBaseDataValid(name, description, instructions, products);
        
        if (resultValidation.IsFailed)
            return Result.Fail<Recipe>(resultValidation.Errors[0]);

        var countProductsByType = new Dictionary<ProductType, byte>();
        
        foreach (var product in products)
        {
            if (!countProductsByType.TryAdd(product.Type, 1))
                countProductsByType[product.Type]++;
        }
        
        if (countProductsByType.Count > 10)
            return Result.Fail<Recipe>("A recipe cannot contain more than 10 different product types.");
        
        var recipe = new Recipe(Guid.NewGuid(), name, description, instructions);
        recipe.AddProducts(products);
        return Result.Ok(recipe);
    }

    private static Result IsBaseDataValid(string name, string description, string instructions,
        IReadOnlyList<Product> products)
    {
        switch (products.Count)
        {
            case 0:
                return Result.Fail("A recipe don't must be empty");
            case > 32:
                return Result.Fail("A recipe cannot contain more than 32 products.");
        }

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            return Result.Fail("Name and Description cannot be empty.");
        
        if (!IsValidName(name))
            return Result.Fail("Name must be between 2 and 128 characters.");
        
        if (!IsValidDescription(description))
            return Result.Fail("Description must be between 2 and 1024 characters.");
        
        if (!IsValidInstructions(instructions))
            return Result.Fail("Instructions must be between 2 and 1024 characters.");
        
        return Result.Ok();
    }
    
    private static bool IsValidName(string name)
    {
        return name.Length is >= 2 and <= 128;
    }
    
    private static bool IsValidDescription(string description)
    {
        return description.Length is >= 2 and <= 1024;
    }
    
    private static bool IsValidInstructions(string instructions)
    {
        return instructions.Length is >= 2 and <= 1024;
    }
}