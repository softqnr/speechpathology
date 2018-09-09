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
            get => _articulationTestAnswers;
            set => SetProperty(ref _articulationTestAnswers, value);
        }
        public double SuccessPercentage
        {
            get => _successPercentage;
            set => SetProperty(ref _successPercentage, value);
        }
        public ICommand ExcelExport
        {
            get
            {
                return new Command(async () =>
                {
                    DialogService.ShowLoading("Generating File Wait …");
                    await Task.Delay(1000);
                    DialogService.HideLoading();
                });
            }
        }
        public PhonologicalTestResultsViewModel()
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
