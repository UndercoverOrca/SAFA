using Safa.Domain;
using Safa.Infrastructure.Transactions;

namespace Safa.Infrastructure;

public static class TransactionFactory
{
    public static Transaction Convert(TransactionEntity entity) =>
        new(
            entity.Id,
            entity.TransactionDate,
            entity.Description,
            entity.TypeOfTransaction,
            entity.Amount,
            entity.IsSpendingMoney);

    public static TransactionEntity Convert(Transaction entity)
    {
        return new TransactionEntity
        {
            Id = entity.Id,
            TransactionDate = entity.Date,
            Description = entity.Description,
            TypeOfTransaction = entity.Type,
            Amount = entity.Amount,
            IsSpendingMoney = entity.IsSpendingMoney
        };
    }
}