using LanguageExt;
using Safa.Domain;

namespace Safa.Application;

public interface ITransactionRepository
{
    Task<Option<IReadOnlyList<Transaction>>> GetAll(Option<Guid> userId);

    Task<Option<TransactionRequest>> GetBy(Guid transactionId, Option<Guid> userId);

    Task Create(TransactionRequest transaction, Guid userId);
    
    Task Update(TransactionRequest transaction, Guid userId);
    
    Task Delete(TransactionRequest transactionRequest, Guid userId);
}