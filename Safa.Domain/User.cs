namespace Safa.Domain;

public record User(
    Guid Guid,
    string FullName,
    MoneyDistribution MoneyDistribution,
    List<Transaction> Transactions
);