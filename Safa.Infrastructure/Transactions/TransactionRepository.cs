using LanguageExt;
using LanguageExt.Common;
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

    // TODO: implement Aff & Option
    public async Task<IReadOnlyList<Transaction>> GetAll(Option<Guid> userId)
    {
        using var dbContext = this.context;

        var transactions = await this.context.Transactions
            // .Where(x => x.UserEntityId == userId)
            .Select(x => TransactionFactory.Convert(x))
            .ToListAsync();
        
        return transactions;
    }

    public async Task<Option<Transaction>> GetBy(Guid transactionId)
    {
        await using var dbContext = this.context;

        var transaction = this.context.Transactions
            .Where(x => x.Id == transactionId)
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
}