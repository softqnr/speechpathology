using SpeechPathology.Models;
using SpeechPathology.Services.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Xamarin.Forms;
using System.Windows.Input;
using SpeechPathology.Interfaces;

namespace SpeechPathology.ViewModels
{
    public class PhonologicalTestResultsViewModel : ViewModelBase
    {
        private IList<ArticulationTestAnswer> _articulationTestAnswers;
        private double _successPercentage;
        public IList<ArticulationTestAnswer> ArticulationTestAnswers {
            get {
                return _articulationTestAnswers;
            }
            set {
                _articulationTestAnswers = value;
                OnPropertyChanged("ArticulationTestAnswers");
            }
        }
        public double SuccessPercentage
        {
            get { 
                return _successPercentage;
            }
            set {
                _successPercentage = value;
                OnPropertyChanged("SuccessPercentage");
            }
        }
        public ICommand ExcelExport
        {
            get
            {
                return new Command(async () =>
                {
                    DependencyService.Get<IMessage>().ShortAlert("Excel export");
                });
            }
        }
        public PhonologicalTestResultsViewModel(INavigationService navigationService) : base(navigationService)
        {
            
        }
        public override Task InitializeAsync(object navigationData)
        {
            ArticulationTestAnswers = (IList<ArticulationTestAnswer>)navigationData;
            SuccessPercentage = _articulationTestAnswers.DefaultIfEmpty().Average(x => Convert.ToInt32(x.IsCorrect));

            return Task.FromResult(true);
        }
    }
}
