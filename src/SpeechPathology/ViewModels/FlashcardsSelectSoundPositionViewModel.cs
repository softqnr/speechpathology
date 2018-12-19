using SpeechPathology.DataServices.Flashcard;
using SpeechPathology.Models.Enums;
using SpeechPathology.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class FlashcardsSelectSoundPositionViewModel : ViewModelBase
    {
        private IFlashcardService _flashcardService;
        private string _selectedSound;
        private string _selectedExcludedSound;
        private List<string> _soundPositions;

        public string SelectedSound
        {
            get => _selectedSound;
            set => SetProperty(ref _selectedSound, value);
        }

        public string SelectedExcludedSound
        {
            get => _selectedExcludedSound;
            set => SetProperty(ref _selectedExcludedSound, value);
        }

        public List<string> SoundPositions
        {
            get => _soundPositions;
            set => SetProperty(ref _soundPositions, value);
        }

        public ICommand ItemTappedCommand
        {
            get
            {
                return new Command<string>(async (s) =>
                {
                    await OnPositionSelected(s);
                });
            }
        }

        public FlashcardsSelectSoundPositionViewModel(IFlashcardService flashcardService)
        {
            _flashcardService = flashcardService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await LoadData(navigationData);
        }

        private async Task LoadData(object navigationData)
        {
            var navigationDataArray = navigationData as string[];
            if (navigationDataArray != null)
            {
                SelectedSound = navigationDataArray[0];
                _selectedExcludedSound = navigationDataArray[1];
                // Get sounds
                SoundPositions = await _flashcardService.GetSoundPositions(SelectedSound, _selectedExcludedSound);
            }
        }

        public async Task OnPositionSelected(string soundPosition)
        {
            await NavigationService.NavigateToAsync<FlashcardsTestViewModel>(new[] { soundPosition, _selectedSound, _selectedExcludedSound });
            await NavigationService.RemoveLastFromBackStackAsync();
        }
    }
}
