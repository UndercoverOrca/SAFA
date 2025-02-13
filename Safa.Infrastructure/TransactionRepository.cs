using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using Microsoft.EntityFrameworkCore;
using Safa.Application;
using Safa.Domain.Extensions;
using Safa.Domain.Types;
using Safa.Infrastructure.Factories;

namespace Safa.Infrastructure;

public class TransactionRepository : ITransactionRepository
{
    private readonly SafaDbContext context;

    public TransactionRepository(SafaDbContext context)
    {
        this.context = context;
    }

    public async Task<Option<IReadOnlyList<Transaction>>> GetAll(Option<Guid> userId)
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

    public async Task<Option<TransactionRequest>> GetBy(Guid transactionId, Option<Guid> userId)
    {
        await using var dbContext = this.context;

        var transaction = this.context.Transactions
            .Where(x => x.Id == transactionId)
            .Where(x => x.UserEntityId == userId.ValueUnsafe())
            .FirstOrNone()
            .Select(TransactionFactory.TryConvert);

        return transaction;
    }

    public async Task Create(TransactionRequest transaction, Guid userId)
    {
        await using var dbContext = this.context;

        var transactionEntity = TransactionFactory
            .TryConvert(transaction, userId);

        await dbContext.Transactions.AddAsync(transactionEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task Update(TransactionRequest transaction, Guid userId)
    {
        await using var dbContext = this.context;
        
        var transactionEntity = TransactionFactory.TryConvert(transaction, userId);

        dbContext.Transactions.Update(transactionEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(TransactionRequest transaction, Guid userId)
    {
        await using var dbContext = this.context;
        
        var transactionEntity = TransactionFactory.TryConvert(transaction, userId);

        dbContext.Transactions.Remove(transactionEntity);
        await dbContext.SaveChangesAsync();
    }
}