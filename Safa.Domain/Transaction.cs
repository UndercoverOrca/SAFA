namespace Safa.Domain;

public record Transaction(
    Guid Id,
    DateTimeOffset Date,
    string Description,
    TypeOfTransaction Type,
    decimal Amount, 
    bool IsSpendingMoney,
    Guid UserId,
    User User);