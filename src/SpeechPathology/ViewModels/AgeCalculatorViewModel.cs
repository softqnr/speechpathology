using SpeechPathology.Models;
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
        private DateTime _endDate;
        private DateTime _startDate;
        private string _resultOut;
        private AgeCalculation _ageCalculation;

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                SetProperty(ref _startDate, value);
                _ageCalculation.BirthDate = _startDate;
                _ageCalculation.Calculate();
                OnPropertyChanged(nameof(CurrentAge));
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                SetProperty(ref _endDate, value);
                _ageCalculation.TestDate = _endDate;
                _ageCalculation.Calculate();
                OnPropertyChanged(nameof(CurrentAge));
            }
        }

        public string CurrentAge
        {
            get
            {
                return string.Format("{0} years {1} months {2} days.",
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

        public AgeCalculatorViewModel() //IAgeCalculatorService ageCalculatorService)
        {
            _ageCalculation = new AgeCalculation();

            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

            LanguageSkillsCommand = new Command(async () => await OpenLanguageSkillsSheet() );
            SpeechSoundsCommand = new Command(async () => await OpenSpeechSoundsSheet() );
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
    }
}
