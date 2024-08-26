namespace Safa.Domain;

public record Transaction(
    Guid Id,
    DateOnly Date,
    string Description,
    TypeOfTransaction Type,
    decimal Amount,
    bool IsSpendingMoney);