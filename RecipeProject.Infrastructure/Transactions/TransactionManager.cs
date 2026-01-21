using FluentResults;
using Microsoft.EntityFrameworkCore.Storage;
using RecipeProject.Application.Abstracts;

namespace RecipeProject.Infrastructure.Transactions;

public class TransactionManager(ApplicationDbContext dbContext) : ITransactionManager
{
    public async Task<Result<ITransactionScope>> BeginTransaction(CancellationToken cancellationToken)
    {
        try
        {
            var dbContextTransaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            var transactionScope = new TransactionScope(dbContextTransaction.GetDbTransaction());
            return transactionScope;
        }
        catch 
        {
            return Result.Fail("Could not begin transaction");
        }
    }

    public async Task<Result> SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
        catch 
        {
            return Result.Fail("Could not save changes to the database");
        }
    }
}