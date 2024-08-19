using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Safa.Domain;

namespace Safa.Infrastructure.Transactions;

public class TransactionEntity
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    [Required]
    public DateOnly TransactionDate { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public TypeOfTransaction TypeOfTransaction { get; set; }
    
    [Required]
    public decimal Amount { get; set; }
    
    [Required]
    public bool IsSpendingMoney { get; set; }
    
    [Required]
    public Guid UserEntityId { get; set; }
    
    [Required]
    public virtual UserEntity UserEntity { get; set; }
}