using System.Transactions;
using SQLite;
using Coursework.Models;

namespace DatabaseService.Services;

public class dbTransaction
{
    private SQLiteAsyncConnection _database;

    public dbTransaction()
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "transaction.db");
        // Updated for customer table
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<TransactionModel>().Wait(); // Create the Customer table if it doesn't exist
    }

    public SQLiteAsyncConnection GetConnection()
    {
        return _database;
    }

    // Add a new Customer
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


    // Get All transactions
    public async Task<List<TransactionModel>> GetTransactionsAsync()
    {
        return await _database.Table<TransactionModel>().ToListAsync(); // Get a list of customers from the database
    }

    // Get transaction By Id
    public async Task<TransactionModel> GetTransactionById(Guid id)
    {
        return await _database.Table<TransactionModel>()
            .FirstOrDefaultAsync(t =>  t.TransactionId == id); // Find customer by email
    }
}