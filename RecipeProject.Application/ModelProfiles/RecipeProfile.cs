using AutoMapper;
using RecipeProject.Application.Models;
using RecipeProject.Domain.Entities.RecipeEntity;

namespace RecipeProject.Application.ModelProfiles;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<Recipe, RecipeDto>();
    }
}