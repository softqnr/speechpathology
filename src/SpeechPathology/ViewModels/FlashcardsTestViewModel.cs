﻿using SpeechPathology.DataServices.Flashcard;
using SpeechPathology.Models;
using SpeechPathology.Models.Enums;
using SpeechPathology.Services.Navigation;
using SpeechPathology.Services.Sound;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

                await LoadData(navigationDataArray[0], navigationDataArray[1]);
            }
        }

        private async Task LoadData(string sound, string excludedSound)
        {
            // Get flashcards
            Flashcards = new ObservableCollection<Flashcard>(_flashcardService.GetFlashcards(SoundPosition.Initial, sound, excludedSound));
       
            //ImageSource = ImageSource.FromFile("cherries.jpg");
            //Text = "Cheries";
            //Sound = "bike.mp3";
            await Task.FromResult(true);
        }
        public async Task OnPlaySound(string fileName)
        {
            await _soundService.PlaySoundAsync(fileName);
        }
    }
}
