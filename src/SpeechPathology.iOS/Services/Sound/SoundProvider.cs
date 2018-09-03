using System.IO;
using System.Threading.Tasks;
using AVFoundation;
using Foundation;
using SpeechPathology.iOS.Services.Sound;
using SpeechPathology.Services.Sound;
using Xamarin.Forms;

[assembly: Dependency(typeof(SoundProvider))]
namespace SpeechPathology.iOS.Services.Sound
{
    public class SoundProvider : NSObject, ISoundService
    {
        private AVAudioPlayer _player;

        public Task PlaySoundAsync(string filename)
        {
            var tcs = new TaskCompletionSource<bool>();

            string path = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(filename),
                Path.GetExtension(filename));

            var url = NSUrl.FromString(path);
            _player = AVAudioPlayer.FromUrl(url);

            _player.FinishedPlaying += (object sender, AVStatusEventArgs e) => {
                _player = null;
                tcs.SetResult(true);
            };

            _player.Play();

            return tcs.Task;
        }
    }
}