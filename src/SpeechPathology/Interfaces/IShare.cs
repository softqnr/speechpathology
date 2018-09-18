using System.Threading.Tasks;

namespace SpeechPathology.Interfaces
{
    public interface IShare
    {
        void ShareFile(string title, string message, string filePath);
    }
}
