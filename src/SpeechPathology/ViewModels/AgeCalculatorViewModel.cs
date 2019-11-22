using SpeechPathology.Models;
using SpeechPathology.Resources;
using SpeechPathology.Services.AgeCalculator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class AgeCalculatorViewModel : ViewModelBase
    {
        private readonly IAgeCalculatorService _ageCalculatorService;
        private List<AgeCalculation> _ageCalculations;

        private DateTime _endDate;
        private DateTime _startDate;

        public List<AgeCalculation> AgeCalculations
        {
            get => _ageCalculations;
            set => SetProperty(ref _ageCalculations, value);
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                SetProperty(ref _startDate, value);
                Application.Current.Properties["StartDate"] = value;
                Application.Current.SavePropertiesAsync();
                CalculateAge();
                OnPropertyChanged(nameof(CurrentAgeString));
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                SetProperty(ref _endDate, value);
                CalculateAge();
                OnPropertyChanged(nameof(CurrentAgeString));
            }
        }

        public string CurrentAgeString
        {
            get
            {
                string y = (AgeInYears == 1) ? AppResources.YearSingular : AppResources.YearPlural;
                string m = (MonthsThisYear == 1) ? AppResources.MonthSingular : AppResources.MonthPlural;
                string d = (DaysThisMonth == 1) ? AppResources.DaySingular : AppResources.DayPlural;

                return string.Format("{0} " + y + " {1} " + m + " {2} " + d + ".",
                    AgeInYears,
                    MonthsThisYear,
                    DaysThisMonth);
            }
        }

        public bool IsValidAge
        {
            get
            {
                var rslt = AgeCalculations.FindIndex(x => x.AgeInYears >= AgeInYears && x.Months >= MonthsThisYear);
                return (rslt != 0);
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

        public ICommand SpeechSoundsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await OnSpeechSoundsSelected();
                });
            }
        }

        public AgeCalculatorViewModel(IAgeCalculatorService ageCalculatorService)
        {
            _ageCalculatorService = ageCalculatorService;

            StartDate = Application.Current.Properties.ContainsKey("StartDate") ?
                (DateTime)Application.Current.Properties["StartDate"] :
                DateTime.Today;
            EndDate = DateTime.Today;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            // Get ageCalculation model data
            var ageCalculations = await _ageCalculatorService.GetAllAsync(App.Language);
            AgeCalculations = ageCalculations;
        }

        private async Task OnLanguageSkillsSelected()
        {
            RefreshAgeCalculation();

            DialogService.ShowLoading(AppResources.Loading);
            await NavigationService.NavigateToAsync<AgeCalcPdfViewerViewModel>(ageCalculation.LanguageSkillsFile);
            DialogService.HideLoading();
        }

        private async Task OnSpeechSoundsSelected()
        {
            RefreshAgeCalculation();

            if (IsValidAge)
            {
                DialogService.ShowLoading(Resources.AppResources.Loading);
                string[] array = { ageCalculation.SpeechSoundsFile, AgeInYears.ToString(), MonthsThisYear.ToString() };
                await NavigationService.NavigateToAsync<AgeCalcSpeechSoundsViewModel>(array);
                DialogService.HideLoading();
            }
            else
            {
                await DialogService.ShowAlertAsync(
                    Resources.AppResources.AgeNotSetMsg,
                    Resources.AppResources.AgeNotSetTitle,
                    Resources.AppResources.Continue);
            }
        }

        public int AgeInYears { get; set; }
        public int MonthsThisYear { get; set; }
        public int DaysThisMonth { get; set; }
        public int TotalDays { get; set; }
        public int DaysThisYear { get; set; }
        public AgeCalculation ageCalculation;

        public void CalculateAge()
        {
            DateTime d;
            var start = StartDate;
            var end = EndDate;

            TotalDays = EndDate.Subtract(StartDate).Days;

            AgeInYears = 0;
            d = start;
            while (d <= end)
            {
                AgeInYears++;
                d = start.AddYears(AgeInYears);
            }
            AgeInYears--;
            start = start.AddYears(AgeInYears);

            DaysThisYear = 0;
            d = start;
            while (d <= end)
            {
                DaysThisYear++;
                d = start.AddDays(DaysThisYear);
            }
            DaysThisYear--;

            MonthsThisYear = 0;
            d = start;
            while (d <= end)
            {
                MonthsThisYear++;
                d = start.AddMonths(MonthsThisYear);
            }
            MonthsThisYear--;
            start = start.AddMonths(MonthsThisYear);

            DaysThisMonth = 0;
            d = start;
            while (d <= end)
            {
                DaysThisMonth++;
                d = start.AddDays(DaysThisMonth);
            }
            DaysThisMonth--;
        }

        private void RefreshAgeCalculation()
        {
            for (var i = 0; i < AgeCalculations.Count; i++)
            {
                if (AgeCalculations[i].AgeInYears > AgeInYears)
                    break;
                ageCalculation = AgeCalculations[i];
            }
        }
    }
}
