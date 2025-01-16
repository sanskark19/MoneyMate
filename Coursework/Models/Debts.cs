using System;
using System.ComponentModel.DataAnnotations;
using SQLite;

namespace Coursework.Models
{
    public class Debts
    {
        [PrimaryKey, Column("DebtId"), Unique]
        public Guid DebtId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Source is required.")]
        public string Source { get; set; }

        [Required(ErrorMessage = "Transaction Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Due date is required.")]
        public DateTime DueDate { get; set; }

        public DateTime ClearedDate { get; set; } = DateTime.Now;

        public bool IsCleared { get; set; } = false;
    }
}