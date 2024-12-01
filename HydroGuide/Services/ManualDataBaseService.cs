using HydroGuide.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroGuide.Services
{
    public class ManualDatabaseService
    {
        private readonly SQLiteConnection _database;

        public ManualDatabaseService()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ToDoLists.db");
            _database = new SQLiteConnection(databasePath);
            _database.CreateTable<ManualObject>();
        }

        // Asynchronous Insert operation
        public async Task<int> AddRecord(ManualObject model)
        {
            return await Task.Run(() => _database.Insert(model));
        }

        // Fetch all records
        public Task<List<ManualObject>> GetAllRecordsAsync()
        {
            return Task.Run(() =>
            {
                if (_database == null)
                    throw new InvalidOperationException("Database connection is not initialized.");

                var records = _database.Table<ManualObject>().ToList();
                return records ?? []; // Return an empty list if null
            });
        }

        // Delete a record by Id
        public async Task<int> DeleteRecord(int id)
        {
            return await Task.Run(() => _database.Delete<ManualObject>(id));
        }

        public Task<int> UpdateRecord(ManualObject model)
        {
            return Task.Run(() => _database.Update(model));
        }

    }
}
