using FluentResults;
using RecipeProject.Domain.Entities.RecipeEntity;

namespace RecipeProject.Domain.Entities.ProductEntity;

public class Product : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ProductType Type { get; private set; }

    public Product(Guid id, string name, string description, ProductType type)
    {
        Id = id;
        Name = name;
        Description = description;
        Type = type;
    }

    public static Result<Product> Create(string name, string description, ProductType type)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
        {
            return Result.Fail("Name and Description cannot be empty.");
        }
        
        if (!IsValidName(name))
            return Result.Fail("Name must be between 2 and 128 characters.");
        
        if (!IsValidDescription(description))
            return Result.Fail("Description must be between 2 and 1024 characters.");
        
        var product = new Product(Guid.NewGuid(), name, description, type);
        return Result.Ok(product);
    }

    private static bool IsValidName(string name)
    {
        return name.Length is >= 2 and <= 128;
    }
    
    private static bool IsValidDescription(string description)
    {
        return description.Length is >= 2 and <= 1024;
    }
}