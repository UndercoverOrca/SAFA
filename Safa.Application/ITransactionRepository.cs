using LanguageExt;
using Safa.Domain;

namespace Safa.Application;

public interface ITransactionRepository
{
    Task<IReadOnlyList<Transaction>> GetAll();

    Task<Option<Transaction>> GetBy(Guid transactionId);

    Task Create(Transaction transaction);
}