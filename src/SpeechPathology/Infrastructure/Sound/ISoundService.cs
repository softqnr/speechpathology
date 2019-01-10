using System.Threading.Tasks;

namespace SpeechPathology.Infrastructure.Sound
{
    public interface ISoundService
    {
        Task PlaySoundAsync(string filename);
    }
}
