using SpeechPathology.Models;
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
            set => SetProperty(ref _speechSoundsFile, value);
        }

        private Tuple<int, int> ageLimit;
        private Tuple <int, int> age;
        private AgeCalculation ageCalculation;

        public string SpeechSoundsDetail
        {
            get => _speechSoundsDetail;
            set => SetProperty(ref _speechSoundsDetail, value);
        }

        public ICommand AgeSpecificTestCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await OnPerformTest();
                });
            }
        }

        private AgeCalculation _ageCalculation;

        public AgeCalcSpeechSoundsViewModel() {}

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                string[] navigationDataArray = (string[])navigationData;

                SpeechSoundsFile = navigationDataArray[0];
                var yearLimitString = navigationDataArray[1];
                var monthLimitString = navigationDataArray[2];
                ageLimit = Tuple.Create(int.Parse(yearLimitString), int.Parse(monthLimitString));
                var ageString = navigationDataArray[3];
                var monthString = navigationDataArray[4];
                age = Tuple.Create(int.Parse(ageString), int.Parse(monthString));
                SpeechSoundsDetail = AppResources.SpeechSoundsDetail;
            }
            await Task.FromResult(true);
        }

        private async Task OnPerformTest()
        {
            if (age.Item1 >= ageLimit.Item1 && age.Item2 >= ageLimit.Item2)
            {
                DialogService.ShowLoading(Resources.AppResources.Loading);
                await NavigationService.NavigateToAsync<ArticulationTestViewModel>(age);
                DialogService.HideLoading();
            }
            else
                await DialogService.ShowAlertAsync(
                    Resources.AppResources.AgeNotSetMsg,
                    Resources.AppResources.AgeNotSetTitle,
                    Resources.AppResources.Continue);
        }
    }
}
