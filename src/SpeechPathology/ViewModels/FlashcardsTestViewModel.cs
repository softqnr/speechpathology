using SpeechPathology.DataServices.Flashcard;
using SpeechPathology.Models;
using SpeechPathology.Models.Enums;
using SpeechPathology.Services.Navigation;
using SpeechPathology.Services.Sound;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class FlashcardsTestViewModel : ViewModelBase
    {
        private IFlashcardService _flashcardService;
        private ISoundService _soundService;
        private IList<Flashcard> _flashcards;
        private ImageSource _imageSource;
        private string _text;
        private string _sound;
        private string _selectedSound;

        public IList<Flashcard> Flashcards
        {
            get => _flashcards;
            set => SetProperty(ref _flashcards, value);
        }

        public ImageSource ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public string Sound
        {
            get => _sound;
            set => SetProperty(ref _sound, value);
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

                await LoadData(navigationDataArray[0], navigationDataArray[1]);
            }
        }

        private async Task LoadData(string sound, string excludedSound)
        {
            // Get flashcards
            //Flashcards = _flashcardService.GetFlashcards(SoundPosition.Initial, sound, excludedSound);
            ImageSource = ImageSource.FromFile("cherries.jpg");
            Text = "Cheries";
            Sound = "bike.mp3";
            await Task.FromResult(true);
        }
        public async Task OnPlaySound(string fileName)
        {
            await _soundService.PlaySoundAsync(fileName);
        }
    }
}
