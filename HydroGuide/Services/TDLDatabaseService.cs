﻿using HydroGuide.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroGuide.Services
{
    public class TDLDatabaseService
    {
        private readonly SQLiteConnection _database;

        public TDLDatabaseService()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ToDoLists.db");
            _database = new SQLiteConnection(databasePath);
            _database.CreateTable<TDLObject>();
        }

        // Asynchronous Insert operation
        public async Task<int> AddRecord(TDLObject model)
        {
            return await Task.Run(() => _database.Insert(model));
        }

        // Fetch all records
        public Task<List<TDLObject>> GetAllRecordsAsync()
        {
            return Task.Run(() =>
            {
                if (_database == null)
                    throw new InvalidOperationException("Database connection is not initialized.");

                var records = _database.Table<TDLObject>().ToList();
                return records ?? new List<TDLObject>(); // Return an empty list if null
            });
        }

        // Delete a record by Id
        public async Task<int> DeleteRecord(int id)
        {
            return await Task.Run(() => _database.Delete<TDLObject>(id));
        }
    }
}