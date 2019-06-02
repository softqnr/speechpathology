using System.Threading.Tasks;

namespace SpeechPathology.Interfaces
{
    public interface IFileAccessHelper
    {
        string GetDBPathAndCreateIfNotExists(string databaseFilename);

        Task<string> CopyAssetFileToTemp(string assetFile, string destinationFileName);

        void CopyAssetFileTo(string assetFile, string destinationFileName);
    }
}
