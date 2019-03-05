using SpeechPathology.Models;
using SpeechPathology.Services.AgeCalculator;
using SpeechPathology.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class AgeCalculatorViewModel : ViewModelBase
    {
        private IAgeCalculatorService _ageCalculatorService;

        private DateTime _endDate;
        private DateTime _startDate;
        private string _resultOut;
        private List<AgeCalculation> _ageResultDocs;

        private int m_years;
        private int m_months;
        private int m_days;

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                SetProperty(ref _startDate, value);
                OnPropertyChanged(nameof(CurrentAge));
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                SetProperty(ref _endDate, value);
                OnPropertyChanged(nameof(CurrentAge));
            }
        }

        public string CurrentAge
        {
            get
            {
                Calculate(StartDate, EndDate);
                return string.Format("{0} years {1} months {2} days.", m_years, m_months, m_days);
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

        public List<AgeCalculation> AgeResultDocs
        {
            get => _ageResultDocs;
            set => SetProperty(ref _ageResultDocs, value);
        }

        public Command LanguageSkillsCommand { get; }
        public Command SpeechSoundsCommand { get; }

        public AgeCalculatorViewModel(IAgeCalculatorService ageCalculatorService)
        {
            _ageCalculatorService = ageCalculatorService;

            //NewSesson = new Command(async () => await StartNewSession() );

            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

            LanguageSkillsCommand = new Command(async () => await OpenLanguageSkillsSheet() );
            SpeechSoundsCommand = new Command(async () => await OpenSpeechSoundsSheet() );

            _ageCalculatorService.GetCurrentAge();
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await Task.FromResult(true);
            await LoadData();
        }

        private async Task LoadData()
        {
            // Get AgeResultDocs
            var docs = await _ageCalculatorService.GetAllAsync();
            AgeResultDocs = docs;
        }

        async Task OpenLanguageSkillsSheet()
        {
            Debug.Print("Opening Language Skills Sheet");
            ResultOut = "Opening Language Skills Sheet";
            await Task.Delay(1500);
            ResultOut = string.Empty;
            
        }

        async Task OpenSpeechSoundsSheet()
        {
            Debug.Print("Opening Speech Sounds Development Sheet");
            ResultOut = "Opening Speech Sounds Development Sheet";
            await Task.Delay(1500);
            ResultOut = string.Empty;
        }

        private void Calculate(DateTime start, DateTime end)
        {
            DateTime d;

            m_years = 0;
            d = start;
            while (d <= end)
            {
                m_years++;
                d = start.AddYears(m_years);
            }

            m_years--;
            start = start.AddYears(m_years);

            m_months = 0;
            d = start;
            while (d <= end)
            {
                m_months++;
                d = start.AddMonths(m_months);
            }

            m_months--;
            start = start.AddMonths(m_months);

            m_days = 0;
            d = start;
            while (d <= end)
            {
                m_days++;
                d = start.AddDays(m_days);
            }

            m_days--;
        }
    }
}
