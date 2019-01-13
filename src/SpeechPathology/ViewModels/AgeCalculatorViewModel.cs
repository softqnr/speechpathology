using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class AgeCalculatorViewModel : ViewModelBase
    {
        //private IAgeCalculatorService _ageCalculatorService;

        private DateTime _endDate;
        private DateTime _startDate;
        private string _resultOut;

        private int m_years;
        private int m_months;
        private int m_days;


        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AgeAtTest));
                OnPropertyChanged(nameof(CurrentAge));
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate =  value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AgeAtTest));
                OnPropertyChanged(nameof(CurrentAge));
            }
        }

        public int AgeAtTest
        {
            get => (EndDate - StartDate.Date).Days;
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

        public Command OralSkillsCommand { get; }
        public Command SpeechDevelopmentCommand { get; }

        public AgeCalculatorViewModel ()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

            OralSkillsCommand = new Command(async () => await OpenOralSkillsSheet() );
            SpeechDevelopmentCommand = new Command(async () => await OpenSpeechDevelopmentSheet() );
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await Task.FromResult(true);
        }

        async Task OpenOralSkillsSheet()
        {
            Debug.Print("Opening Oral Skills Sheet");
            ResultOut = "Opening Oral Skills Sheet";
            await Task.Delay(1500);
            ResultOut = string.Empty;
        }

        async Task OpenSpeechDevelopmentSheet()
        {
            Debug.Print("Opening Speech Development Sheet");
            ResultOut = "Opening Speech Development Sheet";
            await Task.Delay(1500);
            ResultOut = string.Empty;
        }


        public void Calculate(DateTime start, DateTime end)
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
