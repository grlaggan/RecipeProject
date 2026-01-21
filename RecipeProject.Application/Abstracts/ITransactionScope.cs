using FluentResults;

namespace RecipeProject.Application.Abstracts;

public interface ITransactionScope : IDisposable
{
    Result Commit();
    Result Rollback();
}