using SpeechPathology.Models.Enums;
using SpeechPathology.Utils;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel() 
        {

        }
        public ICommand ImageTappedCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await NavigationService.NavigateToAsync<AboutViewModel>();
                });
            }
        }
        public ICommand ArticulationTest
        {
            get
            {
                return new Command(async () =>
                {
                    await OpenArticulationTestPopup();
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
                    await NavigationService.NavigateToAsync<FlashcardsSelectSoundViewModel>();
                });
            }
        }

        public ICommand Worksheets
        {
            get
            {
                return new Command(async () =>
                {
                    await NavigationService.NavigateToAsync<WorksheetsViewModel>();
                });
            }
        }

        private async Task OpenArticulationTestPopup()
        {
            // Open dialog box
            string soundLocation = await DialogService.SelectActionAsync(
                Resources.AppResources.SelectSoundPosition,
                Resources.AppResources.SelectSoundPosition,
                Resources.AppResources.Cancel,
                ResourceHelper.TranslateArray(Enum.GetNames(typeof(SoundPosition))));

            // Convert selected action back to enum
            soundLocation = ResourceHelper.GetResourceNameByValue(soundLocation);
            if (soundLocation != Resources.AppResources.Cancel)
            {
                DialogService.ShowLoading(Resources.AppResources.Loading);
                // Convert string value to enum
                SoundPosition soundPosition = (SoundPosition)Enum.Parse(typeof(SoundPosition), soundLocation);
                // Navigate to articulation test
                await NavigationService.NavigateToAsync<ArticulationTestViewModel>(soundPosition);

                DialogService.HideLoading();
            }
        }
    }
}
