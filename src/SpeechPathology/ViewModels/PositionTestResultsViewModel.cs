using SpeechPathology.Interfaces;
using SpeechPathology.Models;
using SpeechPathology.Services.Articulation;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class PositionTestResultsViewModel : ViewModelBase
    {
        private ArticulationTestExam _articulationTestExam;
        private List<ArticulationTestExamAnswer> _articulationTestAnswers;
        private readonly IPDFGeneratorService _pdfGeneratorService;
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
        public PositionTestResultsViewModel(IPDFGeneratorService pdfGeneratorService)
        {
            _pdfGeneratorService = pdfGeneratorService;
        }
        public override Task InitializeAsync(object navigationData)
        {
            ArticulationTestExam = (ArticulationTestExam)navigationData;
            ArticulationTestAnswers = ArticulationTestExam.Answers;
            Score = ArticulationTestExam.Score ?? ArticulationTestExam.Score.Value;
            SoundPosition = ArticulationTestExam.SoundPosition != "" ? ArticulationTestExam.SoundPosition : Resources.AppResources.All.ToUpper();
            return Task.FromResult(true);
        }

        private async Task GenerateAndSharePdfAsync()
        {
            DialogService.ShowLoading(Resources.AppResources.GeneratingPDF);
            string fileName =  await _pdfGeneratorService.GeneratePDFForPositionTestResultsAsync(_articulationTestExam);
            DialogService.HideLoading();
            DependencyService.Get<IShare>().ShareFile(Resources.AppResources.ShareResults, Resources.AppResources.ShareResults, fileName);
        }
    }
}
