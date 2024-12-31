using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using Microsoft.EntityFrameworkCore;
using Safa.Application;
using Safa.Domain;

namespace Safa.Infrastructure.Transactions;

public class TransactionRepository : ITransactionRepository
{
    private readonly SafaDbContext context;

    public TransactionRepository(SafaDbContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyList<Transaction>> GetAll(Option<Guid> userId)
    {
        await using var dbContext = this.context;

        var transactions = await this.context.Transactions
            .Where(x => x.UserEntityId == userId.ValueUnsafe())
            .Select(x => TransactionFactory.Convert(x))
            .ToListAsync();
        
        return transactions
            .OrderBy(x => x.Date)
            .ToList();
    }

    public async Task<Option<Transaction>> GetBy(Guid transactionId, Option<Guid> userId)
    {
        await using var dbContext = this.context;

        var transaction = this.context.Transactions
            .Where(x => x.Id == transactionId)
            .Where(x => x.UserEntityId == userId.ValueUnsafe())
            .FirstOrNone()
            .Select(TransactionFactory.Convert);

        return transaction;
    }

    public async Task Create(Transaction transaction, Guid userId)
    {
        await using var dbContext = this.context;

        var transactionEntity = TransactionFactory.Convert(transaction, userId);

        await dbContext.Transactions.AddAsync(transactionEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task Update(Transaction transaction, Guid userId)
    {
        await using var dbContext = this.context;
        
        var transactionEntity = TransactionFactory.Convert(transaction, userId);

        dbContext.Transactions.Update(transactionEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(Transaction transaction, Guid userId)
    {
        await using var dbContext = this.context;
        
        var transactionEntity = TransactionFactory.Convert(transaction, userId);

        dbContext.Transactions.Remove(transactionEntity);
        await dbContext.SaveChangesAsync();
    }
}