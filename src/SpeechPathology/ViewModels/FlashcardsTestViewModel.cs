using SpeechPathology.Services.Flashcard;
using SpeechPathology.Models;
using SpeechPathology.Models.Enums;
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
        private int _currentIndex;

        public ObservableCollection<Flashcard> Flashcards
        {
            get => _flashcards;
            set => SetProperty(ref _flashcards, value);
        }

        public int CurrentIndex
        {
            get => _currentIndex;
            set => SetProperty(ref _currentIndex, value);
        }

        public ICommand PlaySoundCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await OnPlaySound();
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
                Enum.TryParse<FlashcardSoundPosition>(navigationDataArray[0], out var soundPosition);
                await LoadData(soundPosition, navigationDataArray[1], navigationDataArray[2]);
            }
        }

        private async Task LoadData(FlashcardSoundPosition soundPosition, string sound, string excludedSound)
        {
            // Get flashcards
            var flashcards = await _flashcardService.GetFlashcards(soundPosition, sound, excludedSound, App.Language);
            Flashcards = new ObservableCollection<Flashcard>(flashcards);
        }

        public async Task OnPlaySound()
        {
            if (CurrentIndex > -1 && !string.IsNullOrWhiteSpace(Flashcards[CurrentIndex].SoundFile)) { 
                await _soundService.PlaySoundAsync(Flashcards[CurrentIndex].SoundFile);
            }
        }
    }
}
