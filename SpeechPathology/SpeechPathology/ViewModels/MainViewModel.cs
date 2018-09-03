using SpeechPathology.Services.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
        public ICommand ArticulationTest
        {
            get
            {
                return new Command(async () =>
                {
                    // Open popup
                    await _navigationService.NavigateToPopupAsync<SelectSoundLocationViewModel>(true);                   
                });
            }
        }

        public ICommand AgeCalculator
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateToAsync<AgeCalculatorViewModel>();
                });
            }
        }

        public ICommand Flashcards
        {
            get
            {
                return new Command(async () =>
                {
                    // Open popup
                    await _navigationService.NavigateToAsync<FlashcardsViewModel>();
                });
            }
        }

        public ICommand Worksheets
        {
            get
            {
                return new Command(async () =>
                {
                    // Open popup
                    await _navigationService.NavigateToAsync<WorksheetsViewModel>();
                });
            }
        }
    }
}
