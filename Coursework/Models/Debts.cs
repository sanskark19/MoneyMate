using System.ComponentModel.DataAnnotations;
using SQLite;
namespace Coursework.Models;

public class Debts
{
    public Guid DebtId { get; set; } = Guid.NewGuid();

    [Required]

public string Source { get; set; } 
    
    [Required]
    public decimal Amount { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }
    
    public DateTime ClearedDate { get; set; } = DateTime.Now;
    
    
    public bool IsCleared { get; set; } = false; 
    
}