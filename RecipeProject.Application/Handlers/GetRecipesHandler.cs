using AutoMapper;
using FluentResults;
using MediatR;
using RecipeProject.Application.Abstracts;
using RecipeProject.Application.Models;
using RecipeProject.Application.Queries;

namespace RecipeProject.Application.Handlers;

public class GetRecipesHandler(IRecipesRepository repository, IMapper mapper) : IRequestHandler<GetRecipesQuery, Result<List<RecipeDto>>>
{
    public async Task<Result<List<RecipeDto>>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetRecipesAsync(cancellationToken);

        if (result.IsFailed)
            return Result.Fail(result.Errors[0]);

        return result.Value.Select(r => mapper.Map<RecipeDto>(r)).ToList();
    }
}
