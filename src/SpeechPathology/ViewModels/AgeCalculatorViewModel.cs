using Plugin.Multilingual;
using SpeechPathology.Models;
using SpeechPathology.Resources;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class AgeCalculatorViewModel : ViewModelBase
    {
        private DateTime _endDate;
        private DateTime _startDate;
        private string _resultOut;
        private AgeCalculation _ageCalculation;
        
        public DateTime StartDate
        {
            get => _ageCalculation.StartDate;
            set
            {
                SetProperty(ref _startDate, value);
                _ageCalculation.StartDate = _startDate;
                _ageCalculation.Calculate();
                OnPropertyChanged(nameof(CurrentAgeString));
            }
        }

        public DateTime EndDate
        {
            get => _ageCalculation.EndDate;
            set
            {
                SetProperty(ref _endDate, value);
                _ageCalculation.EndDate = _endDate;
                _ageCalculation.Calculate();
                OnPropertyChanged(nameof(CurrentAgeString));
            }
        }

        public string CurrentAgeString
        {
            get
            {
                string y = (_ageCalculation.AgeInYears == 1) ? AppResources.YearSingular : AppResources.YearPlural;
                string m = (_ageCalculation.MonthsThisYear == 1) ? AppResources.MonthSingular : AppResources.MonthPlural;
                string d = (_ageCalculation.DaysThisMonth == 1) ? AppResources.DaySingular : AppResources.DayPlural;

                return string.Format("{0} " + y + " {1} " + m + " {2} " + d + ".",
                    _ageCalculation.AgeInYears,
                    _ageCalculation.MonthsThisYear,
                    _ageCalculation.DaysThisMonth);
            }
        }

        public string ResultOut
        {
            get => _resultOut;
            set
            {
                _resultOut = value;
                OnPropertyChanged();
            }
        }

        public Command LanguageSkillsCommand { get; }
        public Command SpeechSoundsCommand { get; }

        public AgeCalculatorViewModel(AgeCalculation ageCalculation)
        {
            _ageCalculation = ageCalculation;

            LanguageSkillsCommand = new Command(async () => await OnLanguageSkillsSelected() );
            SpeechSoundsCommand = new Command(async () => await OpenSpeechSoundsSheet() );
       }

        async Task OnLanguageSkillsSelected()
        {
            var ls = CrossMultilingual.Current.CurrentCultureInfo.TwoLetterISOLanguageName;
            if (ls != null)
            {
                ls = "en";
                DialogService.ShowLoading(Resources.AppResources.Loading);
                await NavigationService.NavigateToAsync<PdfViewerViewModel>("LanguageSkills/languageskills_" + ls + "_all.pdf");
                DialogService.HideLoading();
            }            
        }

        async Task OpenSpeechSoundsSheet()
        {
            Debug.Print("Opening Speech Sounds Development Sheet");
            ResultOut = "Opening Speech Sounds Development Sheet";
            await Task.Delay(1500);
            ResultOut = string.Empty;
        }
    }
}
