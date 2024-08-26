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
}