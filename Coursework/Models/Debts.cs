using SQLite;
namespace Coursework.Models;

public class Debts
{
    public Guid DebtId { get; set; } = Guid.NewGuid();
    
    public string Source { get; set; } 
    
    public decimal Amount { get; set; }
    
    public DateTime DueDate { get; set; }
    
    public bool IsCleared { get; set; } = false; 
    
}