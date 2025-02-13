using System.ComponentModel.DataAnnotations;

namespace Safa.Infrastructure.Entities;

public class UserSettingsEntity
{
    [Required]
    public Guid Id { get; set; }
    
    public decimal SavingFraction { get; set; }
    
    [Required]
    public Guid UserEntityId { get; set; }
    
    [Required]
    public virtual UserEntity UserEntity { get; set; }
}