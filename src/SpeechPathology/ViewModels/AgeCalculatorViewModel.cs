using Plugin.Multilingual;
using SpeechPathology.Models;
using SpeechPathology.Resources;
using SpeechPathology.Services.AgeCalculator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class AgeCalculatorViewModel : ViewModelBase
    {
        private DateTime _endDate;
        private DateTime _startDate;
        private string _resultOut;
        private IAgeCalculatorService _ageCalculatorService;
        private List<AgeCalculation> _ageCalculation;

        public List<AgeCalculation> AgeCalculation
        {
            get => _ageCalculation;
            set => SetProperty(ref _ageCalculation, value);
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            { 
                SetProperty(ref _startDate, value);
                Application.Current.Properties["StartDate"] = _startDate;
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
                //Application.Current.Properties["EndDate"] = _endDate;
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

        public string ResultOut
        {
            get => _resultOut;
            set
            {
                _resultOut = value;
                OnPropertyChanged();
            }
        }

        public ICommand LanguageSkillsCommand
        {
            get
            {
                return new Command<AgeCalculation>(async (ac) =>
                {
                    await OnLanguageSkillsSelected(ac);
                });
            }
        }

        public ICommand SpeechSoundsCommand
        {
            get
            {
                return new Command<AgeCalculation>(async (ac) =>
                {
                    await OnSpeechSoundsSelected(ac);
                });
            }
        }

        public AgeCalculatorViewModel(IAgeCalculatorService ageCalculatorService)
        {
            _ageCalculatorService = ageCalculatorService;

            StartDate = Application.Current.Properties.ContainsKey("StartDate") ? (DateTime)Application.Current.Properties["StartDate"] : DateTime.Today;
            //EndDate = Application.Current.Properties.ContainsKey("EndDate") ? (DateTime)Application.Current.Properties["EndDate"] : DateTime.Today;
            EndDate = DateTime.Today;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            // Get ageCalculation model data
            var ageCalculation = await _ageCalculatorService.GetAllAsync();
            AgeCalculation = ageCalculation;
        }

        async Task OnLanguageSkillsSelected(AgeCalculation ac)
        {
            if (ac == null)
            {
                ac = _ageCalculation[0];
                DialogService.ShowLoading(Resources.AppResources.Loading);
                await NavigationService.NavigateToAsync<PdfViewerViewModel>("LanguageSkills/" + ac.LanguageSkillsFile);
                DialogService.HideLoading();
            }
        }

        async Task OnSpeechSoundsSelected(AgeCalculation ac)
        {
            Debug.Print("Opening Speech Sounds Development Sheet");
            ResultOut = "Opening Speech Sounds Development Sheet";
            await Task.Delay(1500);
            ResultOut = string.Empty;
        }

        public int AgeInYears { get; set; }
        public int MonthsThisYear { get; set; }
        public int DaysThisMonth { get; set; }
        public int TotalDays { get; set; }
        public int DaysThisYear { get; set; }

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
    }
}
