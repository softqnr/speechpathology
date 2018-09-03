using System.Threading.Tasks;
using Xamarin.Forms;

namespace SpeechPathology.Services.Sound
{
    public class SoundService
    {
        private ISoundService _soundProvider;
        public SoundService()
        {
            _soundProvider = DependencyService.Get<ISoundService>();
        }

        public Task PlaySoundAsync(string filename)
        {
            return _soundProvider.PlaySoundAsync(filename);
        }
    }
}
