using SpeechPathology.Models;
using SpeechPathology.Infrastructure.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Xamarin.Forms;
using System.Windows.Input;
using SpeechPathology.Interfaces;
using SpeechPathology.Infrastructure.PDF;

namespace SpeechPathology.ViewModels
{
    public class PhonologicalTestResultsViewModel : ViewModelBase
    {
        private ArticulationTestExam _articulationTestExam;
        private List<ArticulationTestExamAnswer> _articulationTestAnswers;
        private IPDFGeneratorService _pdfGeneratorService;
        private double _score;
        private string _soundPosition;

        public ArticulationTestExam ArticulationTestExam
        {
            get => _articulationTestExam;
            set => SetProperty(ref _articulationTestExam, value);
        }

        public List<ArticulationTestExamAnswer> ArticulationTestAnswers {
            get => _articulationTestAnswers;
            set => SetProperty(ref _articulationTestAnswers, value);
        }
        public string SoundPosition
        {
            get => _soundPosition;
            set => SetProperty(ref _soundPosition, value);
        }

        public double Score
        {
            get => _score;
            set => SetProperty(ref _score, value);
        }
        public ICommand PDFExport
        {
            get
            {
                return new Command(async () =>
                {
                    await GenerateAndSharePdfAsync();
                });
            }
        }
        public PhonologicalTestResultsViewModel(IPDFGeneratorService pdfGeneratorService)
        {
            _pdfGeneratorService = pdfGeneratorService;
        }
        public override Task InitializeAsync(object navigationData)
        {
            ArticulationTestExam = (ArticulationTestExam)navigationData;
            ArticulationTestAnswers = ArticulationTestExam.Answers;
            Score = ArticulationTestExam.Score ?? ArticulationTestExam.Score.Value;
            SoundPosition = ArticulationTestExam.SoundPosition;
            return Task.FromResult(true);
        }

        private async Task GenerateAndSharePdfAsync()
        {
            DialogService.ShowLoading("Generating PDF …");
            string fileName =  await _pdfGeneratorService.GeneratePDFAsync(_articulationTestExam);
            DialogService.HideLoading();
            DependencyService.Get<IShare>().ShareFile("Share results", "Share results", fileName);
        }
    }
}
