using System.ComponentModel.DataAnnotations;

namespace Safa.Infrastructure.Entities;

public class UserPreferencesEntity
{
    [Required]
    public Guid Id { get; set; }
    
    public decimal SavingFraction { get; set; }
}