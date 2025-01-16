using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using Coursework.Models;

namespace DatabaseService.Services
{
    public class dbDebt
    {
        private readonly SQLiteAsyncConnection _database;

        public dbDebt()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dbDebt.db");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Debts>().Wait();
        }

        public SQLiteAsyncConnection GetConnection()
        {
            return _database;
        }

        // Add a new debt
        public async Task<bool> SaveDebtsAsync(Debts debts)
        {
            try
            {
                await _database.InsertAsync(debts);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving debt: {ex.Message}");
                return false;
            }
        }

        // Get all debts
        public async Task<List<Debts>> GetDebtsAsync()
        {
            try
            {
                return await _database.Table<Debts>().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching debts: {ex.Message}");
                return new List<Debts>();
            }
        }

        // Get debt by ID
        public async Task<Debts> GetDebtById(Guid id)
        {
            try
            {
                return await _database.Table<Debts>().FirstOrDefaultAsync(d => d.DebtId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching debt by ID: {ex.Message}");
                return null;
            }
        }

        // Update a debt
        public async Task<bool> UpdateDebtAsync(Debts debts)
        {
            try
            {
                await _database.UpdateAsync(debts);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating debt: {ex.Message}");
                return false;
            }
        }

        // Delete a debt
        public async Task<bool> DeleteDebtAsync(Guid id)
        {
            try
            {
                var debt = await GetDebtById(id);
                if (debt != null)
                {
                    await _database.DeleteAsync(debt);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting debt: {ex.Message}");
                return false;
            }
        }
    }
}
