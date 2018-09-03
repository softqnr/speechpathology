using SQLite;
using System;
using System.Threading.Tasks;

namespace SpeechPathology.Interfaces
{
    public interface IFileAccessHelper
    {
        Task<String> GetDBPathAndCreateIfNotExists(string databaseFilename);
    }
}
