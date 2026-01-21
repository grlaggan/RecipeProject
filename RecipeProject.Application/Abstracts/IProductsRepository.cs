using FluentResults;
using RecipeProject.Application.Models;
using RecipeProject.Domain.Entities.ProductEntity;

namespace RecipeProject.Application.Abstracts;

public interface IProductsRepository
{
    Task<Result<Guid>> Create(Product product, CancellationToken cancellationToken);
    Task<Result<List<ProductDto>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result> DeleteAsync(Product product, CancellationToken cancellationToken);
    Task<Result<Product>> GetById(Guid id, CancellationToken cancellationToken);
}