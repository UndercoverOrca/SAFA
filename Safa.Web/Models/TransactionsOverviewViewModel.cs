using Safa.Domain;

namespace Safa.Web.Models;

public record TransactionsOverviewViewModel(
    IReadOnlyList<Transaction> Transactions,
    decimal TotalIncome,
    decimal TotalExpense,
    decimal RemainingSpendingMoney);