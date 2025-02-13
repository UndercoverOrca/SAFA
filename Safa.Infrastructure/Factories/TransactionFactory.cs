using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using Safa.Application;
using Safa.Domain.Types;
using Safa.Infrastructure.Entities;

namespace Safa.Infrastructure.Factories;

public static class TransactionFactory
{
    public static TransactionEntity TryConvert(TransactionRequest transaction, Guid userId)
    {
        var amount = Amount
            .TryCreate(transaction.Amount)
            .Match(x => x, 
                Amount.Zero);
        
        var transactionDto = new Transaction(
            transaction.Id,
            transaction.Date,
            transaction.Description,
            transaction.Type,
            amount,
            transaction.IsSpendingMoney);
        
        return Convert(transactionDto, userId);
    }

    public static TransactionRequest TryConvert(TransactionEntity entity)
    {
        var amount = Amount
            .TryCreate(entity.Amount)
            .Match(x => x.Value, 
                0m);
        
        return new(
            entity.Id,
            entity.TransactionDate,
            entity.Description,
            entity.TypeOfTransaction,
            amount,
            entity.IsSpendingMoney);
    }

    public static Transaction Convert(TransactionEntity entity)
    {
        var amount = Amount
            .TryCreate(entity.Amount)
            .Match(x => x, 
                Amount.Zero);
        
        return new(
            entity.Id,
            entity.TransactionDate,
            entity.Description,
            entity.TypeOfTransaction,
            amount,
            entity.IsSpendingMoney);
    }

    private static TransactionEntity Convert(Transaction entity, Guid userId) =>
        new()
        {
            Id = entity.Id.IsDefault() ? Guid.NewGuid() : entity.Id,
            TransactionDate = entity.Date,
            Description = entity.Description,
            TypeOfTransaction = entity.Type,
            Amount = entity.Amount.Value,
            IsSpendingMoney = entity.IsSpendingMoney.ValueUnsafe(),
            UserEntityId = userId
        };
}