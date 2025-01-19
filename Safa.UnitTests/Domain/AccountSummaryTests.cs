using Safa.Domain;
using Shouldly;
using static LanguageExt.Prelude;

namespace Safa.UnitTests.Domain;

public class AccountSummaryTests
{
    [Theory]
    [InlineData(0.5, 300)]
    [InlineData(0.7, 500)]
    [InlineData(0.1, -100)]
    [InlineData(0.0, -200)]
    public void CreateFromTransactions_WhenGivenValidDecimal_ReturnsValidRemainingSpendingMoney(decimal savingFraction, decimal expectedRemainingSpendingMoney)
    {
        // Arrange
        var fraction = Fraction.TryCreate(savingFraction)
            .Match(
                fraction => fraction,
                Fraction.Zero);
        var transactions = GetMockTransactionsList();
        var spendingPreferences = new SavingPreferences(fraction);

        // Act
        var result = AccountSummary.CreateFromTransactions(transactions, spendingPreferences);

        // Assert
        result.RemainingSpendingMoney.Value.ShouldBe(expectedRemainingSpendingMoney);
    }

    private static IEnumerable<Transaction> GetMockTransactionsList()
    {
        var totalIncome = Amount.TryCreate(1000m)
            .Match(
                amount => amount,
                Amount.Zero);

        var totalExpenses = Amount.TryCreate(500m)
            .Match(
                amount => amount,
                Amount.Zero);

        var totalSpentMoney = Amount.TryCreate(200m)
            .Match(
                amount => amount,
                Amount.Zero);

        return new List<Transaction>
        {
            new(
                Guid.NewGuid(),
                new DateOnly(2015, 01, 01),
                "Test income",
                TypeOfTransaction.Income,
                totalIncome,
                None),
            new(
                Guid.NewGuid(),
                new DateOnly(2015, 01, 02),
                "Test expense",
                TypeOfTransaction.Expense,
                totalExpenses,
                false),
            new(
                Guid.NewGuid(),
                new DateOnly(2015, 01, 03),
                "Test expense",
                TypeOfTransaction.Expense,
                totalSpentMoney,
                true)
        };
    }
}