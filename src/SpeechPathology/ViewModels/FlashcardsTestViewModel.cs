using SpeechPathology.Services.Flashcard;
using SpeechPathology.Models;
using SpeechPathology.Models.Enums;
using SpeechPathology.Infrastructure.Navigation;
using SpeechPathology.Infrastructure.Sound;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class FlashcardsTestViewModel : ViewModelBase
    {
        private IFlashcardService _flashcardService;
        private ISoundService _soundService;
        private ObservableCollection<Flashcard> _flashcards;

        public ObservableCollection<Flashcard> Flashcards
        {
            get => _flashcards;
            set => SetProperty(ref _flashcards, value);
        }

        public ICommand PlaySoundCommand
        {
            get
            {
                return new Command<string>(async (s) =>
                {
                    await OnPlaySound(s);
                });
            }
        }
        public FlashcardsTestViewModel(IFlashcardService flashcardService, ISoundService soundService)
        {
            _flashcardService = flashcardService;
            _soundService = soundService;
        }
        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                string[]  navigationDataArray =  (string[])navigationData;
                Enum.TryParse<SoundPosition>(navigationDataArray[0], out var soundPosition);
                await LoadData(soundPosition, navigationDataArray[1], navigationDataArray[2]);
            }
        }
        private async Task LoadData(SoundPosition soundPosition, string sound, string excludedSound)
        {
            // Get flashcards
            var flashcards = await _flashcardService.GetFlashcards(soundPosition, sound, excludedSound);
            Flashcards = new ObservableCollection<Flashcard>(flashcards);
        }
        public async Task OnPlaySound(string fileName)
        {
            await _soundService.PlaySoundAsync(fileName);
        }
    }
}
