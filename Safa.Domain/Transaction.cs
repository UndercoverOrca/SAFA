namespace Safa.Domain;

public class Transaction
{
    public Guid Id { get; set; }
    
    public DateOnly Date { get; set; }
    
    public string Description { get; set; }
    
    public TypeOfTransaction Type { get; set; }
    
    public decimal Amount { get; set; }
    
    public bool IsSpendingMoney { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; }
}