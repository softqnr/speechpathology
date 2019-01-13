using SpeechPathology.Models.Enums;
using System;
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
                    // Open dialog box
                    string location = await DialogService.SelectActionAsync("Select sound location", 
                        "Select sound location", "Cancel", Enum.GetNames(typeof(SoundPosition)));
                    
                    if (location != "Cancel") {
                        DialogService.ShowLoading("Loading…");
                        // Convert string value to enum
                        SoundPosition soundPosition = (SoundPosition)Enum.Parse(typeof(SoundPosition), location);
                        // Navigate to articulation test
                        await NavigationService.NavigateToAsync<ArticulationTestViewModel>(soundPosition);

                        DialogService.HideLoading();
                    }
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
