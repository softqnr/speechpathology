using SpeechPathology.DataServices.Flashcard;
using SpeechPathology.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpeechPathology.ViewModels
{
    public class FlashcardsViewModel : ViewModelBase
    {
        private IFlashcardService _flashcardService;
        private IList<string> _sounds;

        public IList<string> Sounds
        {
            get => _sounds;
            set => SetProperty(ref _sounds, value);
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
    }
}
