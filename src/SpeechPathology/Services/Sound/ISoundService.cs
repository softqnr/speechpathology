using System.Threading.Tasks;

namespace SpeechPathology.Services.Sound
{
    public interface ISoundService
    {
        Task PlaySoundAsync(string filename);
    }
}
