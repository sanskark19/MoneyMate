using System.Transactions;
using SQLite;
using Coursework.Models;

namespace DatabaseService.Services
{
    public class dbTransaction
    {
        private readonly SQLiteAsyncConnection _database;

        public dbTransaction()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "transaction.db");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TransactionModel>().Wait(); // Create the Transaction table if it doesn't exist
        }

        public SQLiteAsyncConnection GetConnection()
        {
            return _database;
        }

        // Add a new transaction
        public async Task<bool> SaveTransactionAsync(TransactionModel transaction)
        {
            try
            {
                await _database.InsertAsync(transaction);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving transaction: {ex.Message}");
                return false;
            }
        }

        // Get all transactions
        public async Task<List<TransactionModel>> GetTransactionsAsync()
        {
            return await _database.Table<TransactionModel>().ToListAsync(); // Get a list of transactions from the database
        }

        // Get a transaction by ID
        public async Task<TransactionModel> GetTransactionById(Guid id)
        {
            return await _database.Table<TransactionModel>()
                .FirstOrDefaultAsync(t => t.TransactionId == id); // Find transaction by ID
        }

        // Update balance after a transaction (add or clear debts)
        public async Task UpdateBalance(double amount, string transactionType)
        {
            // This logic only affects the balance without inflow or outflow
            var balanceTransaction = new TransactionModel
            {
                TransactionId = Guid.NewGuid(),
                Title = transactionType == "AddDebt" ? "Debt Added" : "Debt Cleared",
                Amount = transactionType == "AddDebt" ? amount : -amount, // Add debt as positive, clear debt as negative
                Date = DateTime.Now,
                TransactionType = "BalanceUpdate" // Custom type to avoid treating as inflow or outflow
            };

            // Save the transaction but avoid showing it in inflow or outflow
            await SaveTransactionAsync(balanceTransaction);
        }


        // Fetch the current transaction balance
        public async Task<decimal> GetCurrentBalanceAsync()
        {
            try
            {
                // Fetch all transactions and calculate the sum of their amounts
                var transactions = await _database.Table<TransactionModel>().ToListAsync();
                return transactions.Sum(t => (decimal)t.Amount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching current balance: {ex.Message}");
                return 0;
            }
        }

    }
}

