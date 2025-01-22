using Microsoft.AspNetCore.Identity;

namespace Safa.Infrastructure.Entities;

public class UserEntity : IdentityUser<Guid>
{
    public string FullName { get; set; }
    
    public decimal SavingFraction { get; set; }
    
    public virtual ICollection<TransactionEntity> Transactions { get; set; }
}