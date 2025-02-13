using Microsoft.AspNetCore.Identity;

namespace Safa.Infrastructure.Entities;

public class UserEntity : IdentityUser<Guid>
{
    public virtual ICollection<TransactionEntity> Transactions { get; set; }
    
    public virtual UserSettingsEntity UserSettings { get; set; }
}