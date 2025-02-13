using Safa.Domain.Types;

namespace Safa.Application;

public record TransactionRequest(
    Guid Id,
    DateOnly Date,
    string Description,
    TypeOfTransaction Type,
    decimal Amount,
    bool IsSpendingMoney);