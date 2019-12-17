using System;
using System.Threading.Tasks;
using Android.Media;
using SpeechPathology.Droid.Services.Sound;
using SpeechPathology.Infrastructure.Sound;
using Xamarin.Forms;

[assembly: Dependency(typeof(SoundProvider))]
namespace SpeechPathology.Droid.Services.Sound
{
    public class SoundProvider : ISoundService
    {
        private MediaPlayer player;

        public async Task PlaySoundAsync(string filename)
        {
            // Create media player
            player ??= new MediaPlayer();

            player.Reset();

            // Set sound volume
            player.SetVolume(1.0f, 1.0f);

            // Open the resource
            var fd = Android.App.Application.Context.Assets.OpenFd(filename);

            // Hook up some events
            player.Prepared += (s, e) => {
                player.Start();
            };

            player.Completion += (sender, e) => {
                Task.FromResult(true);
            };

            // Initialize
            await player.SetDataSourceAsync(fd.FileDescriptor, fd.StartOffset, fd.Length);
            //player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);

            player.PrepareAsync();
        }
    }
}