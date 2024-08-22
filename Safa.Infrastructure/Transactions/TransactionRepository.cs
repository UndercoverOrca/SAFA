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
            .Select(x => new Transaction
            {
                Id = x.Id,
                Date = x.TransactionDate,
                Description = x.Description,
                Type = x.TypeOfTransaction,
                UserId = x.UserEntityId,
                IsSpendingMoney = x.IsSpendingMoney
            })
            .ToListAsync();
        
        return transactions;
    }

    public async Task<Transaction> GetBy(Guid transactionId)
    {
        await using var dbContext = this.context;

        var transaction = await this.context.Transactions
            // .Where(x => x.UserId == ) // TODO: implement when current user ID is availabe
            .FindAsync(transactionId)
            .Select(x => new Transaction
            {
                Id = x.Id,
                Date = x.TransactionDate,
                Description = x.Description,
                Type = x.TypeOfTransaction,
                UserId = x.UserEntityId,
                IsSpendingMoney = x.IsSpendingMoney
            });

        return transaction;
    }
}