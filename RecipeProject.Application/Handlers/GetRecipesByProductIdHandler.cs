using AutoMapper;
using FluentResults;
using MediatR;
using RecipeProject.Application.Abstracts;
using RecipeProject.Application.Models;
using RecipeProject.Application.Queries;

namespace RecipeProject.Application.Handlers;

public class GetRecipesByProductIdHandler(IRecipesRepository repository, IMapper mapper)
    : IRequestHandler<GetRecipesByProductIdQuery, Result<List<RecipeDto>>>
{
    public async Task<Result<List<RecipeDto>>> Handle(GetRecipesByProductIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetRecipesByIdAsync(request.ProductId, cancellationToken);

        if (result.IsFailed)
            return Result.Fail(result.Errors[0]);

        return result.Value.Select(mapper.Map<RecipeDto>).ToList();
    }
}
