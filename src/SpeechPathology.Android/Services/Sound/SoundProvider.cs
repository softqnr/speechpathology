using System.Threading.Tasks;
using Android.Media;
using SpeechPathology.Droid.Services.Sound;
using SpeechPathology.Services.Sound;
using Xamarin.Forms;

[assembly: Dependency(typeof(SoundProvider))]
namespace SpeechPathology.Droid.Services.Sound
{
    public class SoundProvider : ISoundService
    {
        public Task PlaySoundAsync(string filename)
        {
            // Create media player
            var player = new MediaPlayer();

            // Create task completion source to support async/await
            var tcs = new TaskCompletionSource<bool>();

            // Open the resource
            var fd = Android.App.Application.Context.Assets.OpenFd(filename);

            // Hook up some events
            player.Prepared += (s, e) => {
                player.Start();
            };

            player.Completion += (sender, e) => {
                tcs.SetResult(true);
            };

            // Initialize
            player.SetDataSource(fd.FileDescriptor);
            player.Prepare();

            return tcs.Task;
        }

    }
}