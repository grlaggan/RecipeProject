using System.Security.Claims;
using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using RecipeProject.Application.Abstracts;
using RecipeProject.Application.ModelProfiles;
using RecipeProject.Application.Models;
using RecipeProject.Domain.Entities.ProductEntity;

namespace RecipeProject.Infrastructure.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductsRepository(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Create(Product product, CancellationToken cancellationToken)
    {
        await _dbContext.Products.AddAsync(product, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return product.Id;
    }

    public async Task<Result<List<ProductDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var products = await _dbContext.Products.ToListAsync(cancellationToken);
        var productDtos = products.Select(p => _mapper.Map<ProductDto>(p)).ToList();
        
        return productDtos;
    }

    public async Task<Result> DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }

    public async Task<Result<Product>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        
        if (product == null)
            return Result.Fail(new Error("Product not found"));
        
        return product;
    }
}