using SpeechPathology.DataServices.Flashcard;
using SpeechPathology.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class FlashcardsViewModel : ViewModelBase
    {
        private IFlashcardService _flashcardService;
        private IList<string> _sounds;
        private object _lastTappedItem;
        public IList<string> Sounds
        {
            get => _sounds;
            set => SetProperty(ref _sounds, value);
        }
        public ICommand ItemTappedCommand
        {
            get
            {
                return new Command<string>(async (s) =>
                {
                    await OnLetterSelected(s);
                });
            }
        }

        public Color RandomColor
        {
            get => GenerateRandomColor();
        }

        public object LastTappedItem
        {
            get => _lastTappedItem;
            set => SetProperty(ref _lastTappedItem, value);
        }

        public FlashcardsViewModel(IFlashcardService flashcardService)
        {
            _flashcardService = flashcardService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await LoadData();
        }

        private async Task LoadData(/*SoundPosition soundPosition*/)
        {
            // Get sounds
            Sounds = _flashcardService.GetSounds();
            await Task.FromResult(true);
        }
        public async Task OnLetterSelected(string letter)
        {
            var item = LastTappedItem as string;
            if (item != null)
                System.Diagnostics.Debug.WriteLine("Tapped {0}", item);

            await Task.FromResult(true);
        }

        private Color GenerateRandomColor()
        {
            Random random = new Random();

            // to create lighter colors:
            // take a random integer between 0 & 128 (rather than between 0 and 255)
            // and then add 127 to make the color lighter
            int red = random.Next(128) + 127;
            int green = random.Next(128) + 127;
            int blue = random.Next(128) + 127;

            return Color.FromRgba(red, green, blue, 255);
        }
    }
}
