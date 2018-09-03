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
        public SelectSoundLocationViewModel() 
        {
            SelectLocationCommand = new Command<string>(async (s) => await SelectLocationAsync(s));
        }

        private async Task SelectLocationAsync(string location)
        {
            IsBusy = true;
            // Navigate to articulation test
            await NavigationService.NavigateToAsync<ArticulationTestViewModel>(location);
            // Close popup
            await NavigationService.PopAsync(true);
            IsBusy = false;
        }
    }
}
