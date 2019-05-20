using SpeechPathology.Services.Flashcard;
using SpeechPathology.Infrastructure.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class FlashcardsSelectSoundViewModel : ViewModelBase
    {
        private IFlashcardService _flashcardService;
        private List<string> _sounds;
        private string _selectedSound;
        private string _labelText;
        private bool _skipIsVisible;
        public List<string> Sounds
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
        public ICommand SkipClickedCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await OnSkipClicked();
                });
            }
        }
        public string LabelText
        {
            get => _labelText;
            set => SetProperty(ref _labelText, value);
        }

        public bool SkipIsVisible
        {
            get => _skipIsVisible;
            set => SetProperty(ref _skipIsVisible, value);
        }

        public FlashcardsSelectSoundViewModel(IFlashcardService flashcardService)
        {
            _flashcardService = flashcardService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await LoadData(navigationData);
        }

        private async Task LoadData(object navigationData)
        {
            // Get sounds
            Sounds = await _flashcardService.GetSounds(App.Language);

            _selectedSound = navigationData as string;
            if (_selectedSound == null)
            {
                LabelText = Resources.AppResources.Selectsound;
            }
            else
            {
                LabelText = Resources.AppResources.Selectexcludedsound;
                SkipIsVisible = true;
                Sounds.Remove(_selectedSound);
            }

            await Task.FromResult(true);
        }
        public async Task OnLetterSelected(string s)
        {
            if (s != null)
            {
                if (_selectedSound != null)
                {
                    await NavigationService.NavigateToAsync<FlashcardsSelectSoundPositionViewModel>(new[] { _selectedSound, s });
                    await NavigationService.RemoveLastFromBackStackAsync();
                }
                else
                {
                    await NavigationService.NavigateToAsync<FlashcardsSelectSoundViewModel>(s);
                }
            }
        }
        public async Task OnSkipClicked()
        {
            await NavigationService.NavigateToAsync<FlashcardsSelectSoundPositionViewModel>(new[] { _selectedSound, "" });
            await NavigationService.RemoveLastFromBackStackAsync();
        }
    }
}
