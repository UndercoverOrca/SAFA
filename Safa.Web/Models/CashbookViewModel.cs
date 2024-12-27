using Safa.Domain;

namespace Safa.Web.Models;

public record CashbookViewModel(
    IReadOnlyList<Transaction> Transactions,
    decimal TotalIncome,
    decimal TotalExpenses,
    decimal RemainingSpendingMoney);