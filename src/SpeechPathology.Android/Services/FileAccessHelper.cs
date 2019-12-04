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
        public string GetDBPathAndCreateIfNotExists(string databaseName)
        {
            var docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var dbFile = Path.Combine(docFolder, databaseName); 
            
            if (!File.Exists(dbFile))
            {
                // Copy from assets
                CopyAssetFileTo(databaseName, dbFile);
            }
         
            return dbFile;
        }

        public async Task<string> CopyAssetFileToTemp(string assetFile, string destinationFileName)
        {
            destinationFileName = Path.Combine(Path.GetTempPath(), destinationFileName);

            using (var fileStream = new FileStream(destinationFileName, FileMode.OpenOrCreate, FileAccess.Write)) {
                await Android.App.Application.Context.Assets.Open(assetFile).CopyToAsync(fileStream);
            }

            return destinationFileName;
        }

        public void CopyAssetFileTo(string assetFile, string destinationFileName)
        {
            using var fileStream = new FileStream(destinationFileName, FileMode.OpenOrCreate, FileAccess.Write);
            Android.App.Application.Context.Assets.Open(assetFile).CopyTo(fileStream);
        }
    }
}