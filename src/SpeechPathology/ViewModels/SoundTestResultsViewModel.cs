using SpeechPathology.Infrastructure.PDF;
using SpeechPathology.Interfaces;
using SpeechPathology.Models;
using SpeechPathology.Services.Articulation;
using SpeechPathology.Types;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class SoundTestResultsViewModel : ViewModelBase
    {
        private ArticulationTestExam _articulationTestExam;
        private ObservableCollection<Grouping<string, ArticulationTestExamAnswer>> _articulationTestAnswers;
        private readonly IArticulationTestService _articulationTestService;
        private readonly IPDFGeneratorService _pdfGeneratorService;
        private double _score;
        private string _soundPosition;

        public ArticulationTestExam ArticulationTestExam
        {
            get => _articulationTestExam;
            set => SetProperty(ref _articulationTestExam, value);
        }

        public ObservableCollection<Grouping<string, ArticulationTestExamAnswer>> ArticulationTestAnswers
        {
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

        public SoundTestResultsViewModel(IArticulationTestService articulationTestService, 
            IPDFGeneratorService pdfGeneratorService)
        {
            _articulationTestService = articulationTestService;
            _pdfGeneratorService = pdfGeneratorService;
        }

        public override Task InitializeAsync(object navigationData)
        {
            ArticulationTestExam = (ArticulationTestExam)navigationData;
            
            ArticulationTestAnswers = new ObservableCollection<Grouping<string, ArticulationTestExamAnswer>>(_articulationTestService.GenerateGroupings(ArticulationTestExam));
            Score = ArticulationTestExam.Score ?? ArticulationTestExam.Score.Value;
            SoundPosition = ArticulationTestExam.SoundPosition != "" ? ArticulationTestExam.SoundPosition : Resources.AppResources.All.ToUpper();

            return Task.FromResult(true);
        }

        private async Task GenerateAndSharePdfAsync()
        {
            DialogService.ShowLoading(Resources.AppResources.GeneratingPDF);
            string fileName = await _pdfGeneratorService.GeneratePDFForSoundTestResultsAsync(_articulationTestExam,
                ArticulationTestAnswers);
            DialogService.HideLoading();
            DependencyService.Get<IShare>().ShareFile(Resources.AppResources.ShareResults, Resources.AppResources.ShareResults, fileName);
        }
    }
}
