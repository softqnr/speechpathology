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
        private string _currentAge;
        private string _resultOut;

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
                int daysRemaining = AgeAtTest;
                int years = (int) Math.Floor(daysRemaining / 365f);
                daysRemaining = daysRemaining % 365;
                var months = (int) Math.Floor(daysRemaining / 30f);
                daysRemaining = daysRemaining % 30;
                return years.ToString() + "y " + months.ToString() + "m " + daysRemaining + "d";
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
    }
}
