namespace Safa.Domain;

public class User(
    Guid Guid,
    string FullName,
    UserMoneyDistribution userMoneyDistribution,
    List<Transaction> Transactions
);