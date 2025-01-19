using LanguageExt;

namespace Safa.Domain;

public record Transaction(
    Guid Id,
    DateOnly Date,
    string Description,
    TypeOfTransaction Type,
    Amount Amount,
    Option<bool> IsSpendingMoney);