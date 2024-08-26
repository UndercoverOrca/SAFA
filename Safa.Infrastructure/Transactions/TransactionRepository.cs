using LanguageExt;
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
    public async Task<IReadOnlyList<Transaction>> GetAll()
    {
        await using var dbContext = this.context;

        var transactions = await this.context.Transactions
            // .Where(x => x.UserId == ) // TODO: implement when current user ID is availabe
            .Select(x => TransactionFactory.Convert(x))
            .ToListAsync();
        
        return transactions;
    }

    public async Task<Option<Transaction>> GetBy(Guid transactionId)
    {
        await using var dbContext = this.context;

        var transaction = this.context.Transactions
            // .Where(x => x.UserId == ) // TODO: implement when current user ID is availabe
            .FirstOrNone()
            .Select(TransactionFactory.Convert);

        return transaction;
    }
}