using SpeechPathology.Services.Flashcard;
using SpeechPathology.Utils;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class FlashcardsSelectSoundPositionViewModel : ViewModelBase
    {
        private readonly IFlashcardService _flashcardService;
        private string _selectedSound;
        private string _selectedExcludedSound;
        private string[] _soundPositions;

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

        public string[] SoundPositions
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
            if (navigationData is string[] navigationDataArray)
            {
                SelectedSound = navigationDataArray[0];
                _selectedExcludedSound = navigationDataArray[1];
                // Get sound positions
                var soundPositions = await _flashcardService.GetSoundPositions(SelectedSound, _selectedExcludedSound, App.Language);
                var soundPositionsTranslated = ResourceHelper.TranslateArray(soundPositions.ToArray());
                SoundPositions = soundPositionsTranslated;
            }
        }

        public async Task OnPositionSelected(string soundPosition)
        {
            // Convert selected action to resource key
            var soundLocationResourceKey = ResourceHelper.GetResourceNameByValue(soundPosition);
            await NavigationService.NavigateToAsync<FlashcardsTestViewModel>(new[] { soundLocationResourceKey, _selectedSound, _selectedExcludedSound });
        }
    }
}
