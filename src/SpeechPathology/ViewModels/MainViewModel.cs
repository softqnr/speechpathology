using SpeechPathology.Services.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel() 
        {

        }
        public ICommand ArticulationTest
        {
            get
            {
                return new Command(async () =>
                {
                    // Open popup
                    await NavigationService.NavigateToPopupAsync<SelectSoundLocationViewModel>(true);                   
                });
            }
        }

        public ICommand AgeCalculator
        {
            get
            {
                return new Command(async () =>
                {
                    await NavigationService.NavigateToAsync<AgeCalculatorViewModel>();
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
                    await NavigationService.NavigateToAsync<FlashcardsViewModel>();
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
                    await NavigationService.NavigateToAsync<WorksheetsViewModel>();
                });
            }
        }
    }
}
