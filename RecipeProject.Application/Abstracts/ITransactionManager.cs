using FluentResults;

namespace RecipeProject.Application.Abstracts;

public interface ITransactionManager
{
    Task<Result<ITransactionScope>> BeginTransaction(CancellationToken cancellationToken);
    Task<Result> SaveChangesAsync(CancellationToken cancellationToken);
}