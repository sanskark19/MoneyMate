using System.Transactions;
using SQLite;
using Coursework.Models;

namespace DatabaseService.Services;

public class dbDebt
{
    public SQLiteAsyncConnection _database;

    public dbDebt()
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "dbDebt.db");
        // Updated for customer table
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Debts>().Wait(); 
    }

    public SQLiteAsyncConnection GetConnection()
    {
        return _database;
    }

    // Add a new Customer
    public async Task<bool> SaveTransactionAsync(Debts debts)
    {
        try
        {
            await _database.InsertAsync(debts);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving transaction: {ex.Message}");
            return false;
        }
    }


    // Get All transactions
    public async Task<List<Debts>> GetTransactionsAsync()
    {
        return await _database.Table<Debts>().ToListAsync(); // Get a list of customers from the database
    }

    // Get transaction By Id
    public async Task<Debts> GetTransactionById(Guid id)
    {
        return await _database.Table<Debts>()
            .FirstOrDefaultAsync(t =>  t.DebtId == id); // Find customer by email
    }
}