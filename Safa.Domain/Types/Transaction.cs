using LanguageExt;

namespace Safa.Domain.Types;

public record Transaction(
    Guid Id,
    DateOnly Date,
    string Description,
    TypeOfTransaction Type,
    Amount Amount,
    Option<bool> IsSpendingMoney);