using Safa.Domain;

namespace Safa.Application;

public record TransactionRequest(
    Guid Id,
    DateOnly Date,
    string Description,
    TypeOfTransaction Type,
    decimal Amount,
    bool IsSpendingMoney);