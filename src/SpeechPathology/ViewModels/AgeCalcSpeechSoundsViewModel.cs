using SpeechPathology.Models;
using SpeechPathology.Models.Enums;
using SpeechPathology.Resources;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class AgeCalcSpeechSoundsViewModel : ViewModelBase
    {
        private string _speechSoundsFile;
        private string _speechSoundsDetail;

        public string SpeechSoundsFile
        {
            get => _speechSoundsFile;
            set
            {
                SetProperty(ref _speechSoundsFile, value);
            }
        }

        private int age;

        public string SpeechSoundsDetail
        {
            get => _speechSoundsDetail;
            set
            {
                SetProperty(ref _speechSoundsDetail, value);
            }
        }

        public ICommand AgeSpecificTestCommand
        {
            get
            {
                return new Command<AgeCalculation>(async (ac) =>
                {
                    await OnPerformTest(ac);
                });
            }
        }

        public AgeCalcSpeechSoundsViewModel() { }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                string[] navigationDataArray = (string[])navigationData;

                SpeechSoundsFile = navigationDataArray[0];
                var ageString = navigationDataArray[1];
                age = Int32.Parse(ageString);
                SpeechSoundsDetail = AppResources.SpeechSoundsDetail;
            }
            await Task.FromResult(true);
        }

        private async Task OnPerformTest(AgeCalculation ac)
        {
            //// Open dialog box
            //string location = await DialogService.SelectActionAsync(
            //    Resources.AppResources.SelectSoundPosition,
            //    Resources.AppResources.SelectSoundPosition,
            //    Resources.AppResources.Cancel,
            //    Enum.GetNames(typeof(SoundPosition)));

            //if (location != Resources.AppResources.Cancel)
            //{

            DialogService.ShowLoading(Resources.AppResources.Loading);
            //// Convert string value to enum
            //SoundPosition soundPosition = (SoundPosition)Enum.Parse(typeof(SoundPosition), location);
            //// Navigate to articulation test
            await NavigationService.NavigateToAsync<ArticulationTestViewModel>(age);

            DialogService.HideLoading();
            //}
        }
    }
}
