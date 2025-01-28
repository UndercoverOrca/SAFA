using Safa.Domain.Types;

namespace Safa.Web.Models;

public record CashbookViewModel(
    IReadOnlyList<Transaction> Transactions,
    AccountSummary AccountSummary);