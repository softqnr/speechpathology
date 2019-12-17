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
        private string _languageSkillsFile;

        public string SpeechSoundsFile
        {
            get => _speechSoundsFile;
            set => SetProperty(ref _speechSoundsFile, value);
        }
        
        public string LanguageSkillsFile
        {
            get => _languageSkillsFile;
            set => SetProperty(ref _languageSkillsFile, value);
        }

        private Tuple <int, int> age;

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

        public AgeCalcSpeechSoundsViewModel() { }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                string[] navigationDataArray = (string[])navigationData;

                SpeechSoundsFile = navigationDataArray[0];
                LanguageSkillsFile = navigationDataArray[1];
                var ageString = navigationDataArray[2];
                var monthString = navigationDataArray[3];
                age = Tuple.Create(int.Parse(ageString), int.Parse(monthString));
                SpeechSoundsDetail = AppResources.SpeechSoundsDetail;
            }
            await Task.FromResult(true);
        }

        private async Task OnPerformTest()
        {
            try
            {
                DialogService.ShowLoading(Resources.AppResources.Loading);
                await NavigationService.NavigateToAsync<ArticulationTestViewModel>(age);
                DialogService.HideLoading();
            }
            catch
            {
                DialogService.HideLoading();
                await NavigationService.NavigateBackAsync();
                await DialogService.ShowAlertAsync(
                    Resources.AppResources.AgeNotSetMsg,
                    Resources.AppResources.AgeNotSetTitle,
                    Resources.AppResources.Continue);
            }
        }

        public ICommand LanguageSkillsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await OnLanguageSkillsSelected();
                });
            }
        }

        private async Task OnLanguageSkillsSelected()
        {
            DialogService.ShowLoading(AppResources.Loading);
            await NavigationService.NavigateToAsync<AgeCalcPdfViewerViewModel>(LanguageSkillsFile);
            DialogService.HideLoading();
        }
    }
}
