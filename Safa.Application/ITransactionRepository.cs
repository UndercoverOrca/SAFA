using Safa.Domain;

namespace Safa.Application;

public interface ITransactionRepository
{
    Task<IReadOnlyList<Transaction>> GetAll();

    Task<Transaction> GetBy(Guid transactionId);
}