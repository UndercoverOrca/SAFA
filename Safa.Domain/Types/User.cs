namespace Safa.Domain.Types;

public class User(
    Guid Guid,
    string FullName,
    SavingPreferences savingPreferences,
    List<Transaction> Transactions
);