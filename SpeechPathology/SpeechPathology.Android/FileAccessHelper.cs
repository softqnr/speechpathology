using System;
using System.IO;
using System.Threading.Tasks;
using SpeechPathology.Droid;
using SpeechPathology.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileAccessHelper))]
namespace SpeechPathology.Droid
{
    public class FileAccessHelper : IFileAccessHelper
    {
        public async Task<String> GetDBPathAndCreateIfNotExists(String databaseName)
        {
            var docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var dbFile = Path.Combine(docFolder, databaseName); // FILE NAME TO USE WHEN COPIED
            
            if (!File.Exists(dbFile))
            {
                FileStream writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, FileAccess.Write);
                await Android.App.Application.Context.Assets.Open(databaseName).CopyToAsync(writeStream);
            }
            return dbFile;
        }
    }
}