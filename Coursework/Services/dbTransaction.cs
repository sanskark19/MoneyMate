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
            var balanceTransaction = new TransactionModel
            {
                TransactionId = Guid.NewGuid(),
                Title = transactionType == "AddDebt" ? "Debt Added" : "Debt Cleared",
                Amount = transactionType == "AddDebt" ? -amount : amount,
                Date = DateTime.Now,
                TransactionType = transactionType == "AddDebt" ? "Debit" : "Credit"
            };

            await SaveTransactionAsync(balanceTransaction); // Save the balance update as a transaction
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

