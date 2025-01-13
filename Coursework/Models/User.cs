using SQLite;
using System.ComponentModel.DataAnnotations;

namespace Coursework.Models
{
    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } 

        [Required(ErrorMessage = "Customer Name is required.")]
        public string customer_name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Mobile is required.")]
        public string mobile { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string password { get; set; }

        [Required(ErrorMessage = "Currency Type is required.")]
        public string currency_type { get; set; }
    }
}