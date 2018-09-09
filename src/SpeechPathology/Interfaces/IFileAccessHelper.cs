using SQLite;
using System;
using System.Threading.Tasks;

namespace SpeechPathology.Interfaces
{
    public interface IFileAccessHelper
    {
        string GetDBPathAndCreateIfNotExists(string databaseFilename);
    }
}
