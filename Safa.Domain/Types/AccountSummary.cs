using Safa.Domain.Extensions;

namespace Safa.Domain.Types;

public record AccountSummary(
    Amount TotalIncome,
    Amount TotalExpenses,
    Amount RemainingSpendingMoney)
{
    public static AccountSummary CreateFromTransactions(IEnumerable<Transaction> transactions, SavingPreferences savingPreferences)
    {
        var groups = transactions
            .GroupBy(t => t.Type)
            .ToDictionary(g => g.Key, g => g.ToList());

        var savingFraction = savingPreferences.SavingFraction;
        
        var totalIncome = groups.Find(TypeOfTransaction.Income).Match(x => x.Select(x => x.Amount).Sum(), Amount.Zero);
        var totalExpenses = groups.Find(TypeOfTransaction.Expense).Match(x => x.Select(x => x.Amount).Sum(), Amount.Zero);
        
        var spentMoney = groups
            .Find(TypeOfTransaction.Expense)
            .Match(x => x
                .Where(t => t.IsSpendingMoney.Match(s => s, false))
                .Select(t => t.Amount)
                .Sum(),
                Amount.Zero);
        
        var remainingSpendingMoney = CalculateSpendingMoney(totalIncome, spentMoney, savingFraction);
        
        return new AccountSummary(
            totalIncome,
            totalExpenses,
            remainingSpendingMoney);
    }
    
    private static Amount CalculateSpendingMoney(Amount totalIncome, Amount spentMoney, Fraction savingFraction) =>
        totalIncome * savingFraction - spentMoney;
}