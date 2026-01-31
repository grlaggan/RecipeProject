using FluentResults;
using Moq;
using RecipeProject.Application.Abstracts;
using RecipeProject.Application.Commands;
using RecipeProject.Application.Handlers;
using RecipeProject.Application.Models;
using RecipeProject.Application.Queries;
using RecipeProject.Domain.Entities.ProductEntity;

namespace RecipeProject.Tests.Handlers;

public class ProductHandlersTests
{

    [Fact]
    public async Task GetProducts_ReturnsProduct()
    {
        var mockRepository = new Mock<IProductsRepository>();

        var expectedProducts = new List<ProductDto>
        {
            new ProductDto(Guid.NewGuid(), "Milk", "Milk", ProductType.Protein)
        };

        mockRepository
            .Setup(repo => repo.GetAllAsync(CancellationToken.None))
            .ReturnsAsync(Result.Ok(expectedProducts));

        var handler = new GetProductsHandler(mockRepository.Object);

        var query = new GetProductsQuery();

        var result = await handler.Handle(query, CancellationToken.None);
        
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedProducts[0].Id, result.Value[0].Id);
        Assert.Equal(expectedProducts[0].Name, result.Value[0].Name);
        
        mockRepository.Verify(repo => repo.GetAllAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task CreateProduct_ValidCommand_ReturnsGuid()
    {
        var mockRepository = new Mock<IProductsRepository>();
        
        mockRepository
            .Setup(repo => repo.Create(It.IsAny<Product>(), CancellationToken.None))
            .ReturnsAsync(Result.Ok(Guid.NewGuid()));

        var handler = new CreateProductHandler(mockRepository.Object);
        var command = new CreateProductCommand("Milk", "Milk", ProductType.Protein);

        var result = await handler.Handle(command, CancellationToken.None);
        
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);
        
        mockRepository.Verify(repo => 
                repo.Create(It.IsAny<Product>(), CancellationToken.None),
                Times.Once
            );
    }

    [Fact]
    public async Task CreateProductWithEmptyName_ValidCommand_ReturnsFailedResult()
    {
        var mockRepository = new Mock<IProductsRepository>();
        mockRepository
            .Setup(repo => repo.Create(It.IsAny<Product>(), CancellationToken.None))
            .ReturnsAsync(Result.Ok(Guid.NewGuid()));
        
        var handler = new CreateProductHandler(mockRepository.Object);
        var command = new CreateProductCommand("", "Milk", ProductType.Protein);

        var result = await handler.Handle(command, CancellationToken.None);
        
        Assert.False(result.IsSuccess);
    }
}