// DatabaseServices.cs
using SQLite;
using Coursework.Models;  // Correct the namespace for Customer
using System.IO;
using System.Threading.Tasks;

namespace DatabaseService.Services
{
    public class DatabaseServices
    {
        private SQLiteAsyncConnection _database;

        public DatabaseServices()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "customers.db");
            // Updated for customer table
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Customer>().Wait(); // Create the Customer table if it doesn't exist
        }

        public SQLiteAsyncConnection GetConnection()
        {
            return _database;
        }

        // Add a new Customer
        public async Task<bool> SaveCustomerAsync(Customer customer)
        {
            try
            {
                await _database.InsertAsync(customer);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving customer: {ex.Message}");
                return false;
            }
        }


        // Get All Customers
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _database.Table<Customer>().ToListAsync(); // Get a list of customers from the database
        }

        // Get Customer by Email
        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            return await _database.Table<Customer>()
                .FirstOrDefaultAsync(c => c.email == email); // Find customer by email
        }
    }
}