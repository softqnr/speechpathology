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
        private string _selectedSound;
        private string _labelText;
        private bool _skipIsVisible;
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
        public object LastTappedItem
        {
            get => _lastTappedItem;
            set => SetProperty(ref _lastTappedItem, value);
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

        public FlashcardsViewModel(IFlashcardService flashcardService)
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
            Sounds = _flashcardService.GetSounds();

            _selectedSound = navigationData as string;
            if (_selectedSound == null)
            {
                LabelText = "Select sound";
            }
            else
            {
                LabelText = "Select excluded sound";
                SkipIsVisible = true;
                Sounds.Remove(_selectedSound);
            }
    
            await Task.FromResult(true);
        }
        public async Task OnLetterSelected(string s)
        {
            var item = LastTappedItem as string;
            if (item != null)
            {
                if (_selectedSound != null)
                {
                    //await NavigationService.NavigateToAsync<FlashcardsViewModel>(item);
                    DialogService.ShowToast(s, 1000);
                }
                else
                {
                    await NavigationService.NavigateToAsync<FlashcardsViewModel>(item);
                }
            }
        }
    }
}
