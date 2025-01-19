using Safa.Domain;

namespace Safa.Web.Models;

public record CashbookViewModel(
    IReadOnlyList<Transaction> Transactions,
    AccountSummary AccountSummary);