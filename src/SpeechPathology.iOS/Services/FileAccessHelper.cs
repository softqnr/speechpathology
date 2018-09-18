using System.IO;
using Foundation;
using SpeechPathology.Interfaces;
using SpeechPathology.iOS.Services;

[assembly: Xamarin.Forms.Dependency(typeof(FileAccessHelper))]
namespace SpeechPathology.iOS.Services
{
    public class FileAccessHelper : IFileAccessHelper
    {
        public string GetDBPathAndCreateIfNotExists(string databaseFilename)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, databaseFilename);
            if (!File.Exists(path))
            {

                var existingDb = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(databaseFilename)
                    , Path.GetExtension(databaseFilename));
                File.Copy(existingDb, path);
            }
            return path;
        }
    } 
}