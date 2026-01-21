using AutoMapper;
using RecipeProject.Application.Models;
using RecipeProject.Domain.Entities.ProductEntity;

namespace RecipeProject.Application.ModelProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
    }
}