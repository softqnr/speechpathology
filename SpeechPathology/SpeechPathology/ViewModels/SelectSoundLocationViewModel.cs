using SpeechPathology.Services.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class SelectSoundLocationViewModel : ViewModelBase
    {
        public ICommand SelectLocationCommand { get; private set; }
        public ICommand ResumeTestCommand { get; private set; }
        public SelectSoundLocationViewModel(INavigationService navigationService) : base(navigationService)
        {
            SelectLocationCommand = new Command<string>(async (s) => await SelectLocationAsync(s));
        }

        private async Task SelectLocationAsync(string location)
        {
            IsBusy = true;
            // Navigate to articulation test
            await _navigationService.NavigateToAsync<ArticulationTestViewModel>(location);
            // Close popup
            await _navigationService.PopAsync(true);
            IsBusy = false;
        }
    }
}
