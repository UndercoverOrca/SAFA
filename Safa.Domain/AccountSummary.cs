namespace Safa.Domain;

public record AccountSummary(
    decimal TotalIncome,
    decimal TotalExpenses,
    decimal RemainingSpendingMoney)
{
    public static AccountSummary CreateFromTransactions(IEnumerable<Transaction> transactions, SavingPreferences savingPreferences)
    {
        var groups = transactions
            .GroupBy(t => t.Type)
            .ToDictionary(g => g.Key, g => g.ToList());

        var savingFraction = savingPreferences.SavingFraction;
        
        var totalIncome = groups.Find(TypeOfTransaction.Income).Match(x => x.Sum(t => t.Amount), 0m);
        var totalExpenses = groups.Find(TypeOfTransaction.Expense).Match(x => x.Sum(t => t.Amount), 0m);
        
        var spentMoney = groups
            .Find(TypeOfTransaction.Expense)
            .Match(x => x
                .Where(t => t.IsSpendingMoney.Match(s => s, false))
                .Sum(t => t.Amount),
                0m);

        var remainingSpendingMoney = CalculateSpendingMoney(totalIncome, spentMoney, savingFraction);
        
        return new AccountSummary(
            totalIncome,
            totalExpenses,
            remainingSpendingMoney);
    }
    
    private static decimal CalculateSpendingMoney(decimal totalIncome, decimal spentMoney, decimal savingFraction) =>
        totalIncome * savingFraction - spentMoney;
}