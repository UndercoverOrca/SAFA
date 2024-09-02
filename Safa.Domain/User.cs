namespace Safa.Domain;

public class User(
    Guid Guid,
    string FullName,
    MoneyDistribution MoneyDistribution,
    List<Transaction> Transactions
);