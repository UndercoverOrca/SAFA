using LanguageExt;
using Safa.Domain;

namespace Safa.Application;

public interface ITransactionRepository
{
    Task<IReadOnlyList<Transaction>> GetAll(Option<Guid> userId);

    Task<Option<Transaction>> GetBy(Guid transactionId, Option<Guid> userId);

    Task Create(Transaction transaction, Guid userId);
    
    Task Update(Transaction transaction, Guid userId);
}