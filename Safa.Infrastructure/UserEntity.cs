using Microsoft.AspNetCore.Identity;
using Safa.Infrastructure.Transactions;

namespace Safa.Infrastructure;

public class UserEntity : IdentityUser<Guid>
{
    public virtual ICollection<TransactionEntity> Transactions { get; set; }
}