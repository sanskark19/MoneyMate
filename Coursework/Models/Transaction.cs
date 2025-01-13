using SQLite;
namespace Coursework.Models;

public class TransactionModel
{
    public Guid TransactionId { get; set; } = Guid.NewGuid();
    
    public string Title { get; set; }
    
    public decimal Amount { get; set; }
    
    public string TransactionType  { get; set; }
    
    public string Notes { get; set; }
    
    public string Tags { get; set; } 
    
    public DateTime Date { get; set; }
    
}