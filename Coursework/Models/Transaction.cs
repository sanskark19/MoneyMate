using System.ComponentModel.DataAnnotations;
using SQLite;
namespace Coursework.Models;

public class TransactionModel
{
    public Guid TransactionId { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "Transaction Title is required.")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Transaction Amount is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public double Amount { get; set; }

    [Required(ErrorMessage = "Transaction Type is required.")]
    public string TransactionType { get; set; } = "Credit";
    
    public string Notes { get; set; }
    
    public string Tags { get; set; } 
    
    [Required(ErrorMessage = "Transaction Date is required.")]
    public DateTime Date { get; set; } = DateTime.Now;
    
}