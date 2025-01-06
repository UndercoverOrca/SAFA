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
        var transactions = GetMockTransactionsList();
        var spendingPreferences = new SavingPreferences(savingFraction);

        // Act
        var result = AccountSummary.CreateFromTransactions(transactions, spendingPreferences);

        // Assert
        result.RemainingSpendingMoney.ShouldBe(expectedRemainingSpendingMoney);
    }

    [Fact]
    public void CreateFromTransactions_WhenSavingFractionIsNull_()
    {
        // Arrange


        // Act


        // Assert
    }
    
    [Fact]
    public void CreateFromTransactions_WhenSavingFractionIsNegative_()
    {
        // Arrange


        // Act


        // Assert
    }

    private static IEnumerable<Transaction> GetMockTransactionsList() =>
    [
        new(
            Guid.NewGuid(),
            new DateOnly(2015, 01, 01),
            "Test income",
            TypeOfTransaction.Income,
            1000m,
            None),
        new(
            Guid.NewGuid(),
            new DateOnly(2015, 01, 02),
            "Test expense",
            TypeOfTransaction.Expense,
            500m,
            false),
        new(
            Guid.NewGuid(),
            new DateOnly(2015, 01, 03),
            "Test expense",
            TypeOfTransaction.Expense,
            200m,
            true)
    ];
}