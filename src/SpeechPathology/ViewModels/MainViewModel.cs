using SpeechPathology.Models;
using SpeechPathology.Models.Enums;
using System;
using System.Linq;
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
                    //// Open dialog box
                    //string location = await DialogService.SelectActionAsync(
                    //    Resources.AppResources.SelectSoundPosition,
                    //    Resources.AppResources.SelectSoundPosition, 
                    //    Resources.AppResources.Cancel, 
                    //    Enum.GetNames(typeof(SoundPosition)));

                    //if (location != Resources.AppResources.Cancel) {
                    //    DialogService.ShowLoading(Resources.AppResources.Loading);
                    //    // Convert string value to enum
                    //    SoundPosition soundPosition = (SoundPosition)Enum.Parse(typeof(SoundPosition), location);
                    //    // Navigate to articulation test
                    //    await NavigationService.NavigateToAsync<ArticulationTestViewModel>(soundPosition);

                    //    DialogService.HideLoading();
                    //}

                    //if (AgeCalculatorViewModel.IsValidAge)
                    //{
                        await DialogService.ShowAlertAsync(
                            Resources.AppResources.AgeNotSetMsg,
                            Resources.AppResources.AgeNotSetTitle,
                            Resources.AppResources.Continue);
                    //}
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
    }
}
