using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechPathology.Models;
using SQLite;

namespace SpeechPathology.Data
{
    public class DatabaseContext
    {
        readonly SQLiteAsyncConnection database;

        public DatabaseContext(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
            //database.CreateTableAsync<ArticulationTest>().Wait();
        }
    }
}
