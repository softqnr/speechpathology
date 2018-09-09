using SpeechPathology.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using SpeechPathology.DataServices.Articulation;
using SpeechPathology.Models.Enums;

namespace SpeechPathology.ViewModels
{
    public class ArticulationTestViewModel : ViewModelBase
    {
        private IArticulationService _articulationService;
        private IList<ArticulationTestAnswer> _articulationTestAnswers;
        private IEnumerator<ArticulationTestAnswer> _articulationTestAnswersEnumerator;
        private ArticulationTestAnswer _articulationTestAnswer;

        private string _testIndex;
        private int _testCount;
        private string _text;
        private ImageSource _imageSource;
        private string _image;

        public ICommand AnswerTestCommand { get; private set; } //=> new AsyncCommand(AnswerAsync);

        public string TestIndex
        {
            get => _testIndex;
            set => SetProperty(ref _testIndex, value);
        }

        public int TestCount
        {
            get => _testCount;
            set => SetProperty(ref _testCount, value);
        }

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public ImageSource ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        public string Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public ArticulationTestViewModel(IArticulationService articulationService) 
        {
            _articulationService = articulationService;
            AnswerTestCommand = new Command<bool>(async (b) => await AnswerTestAsync(b)); 
        }

        private async Task AnswerTestAsync(bool answer)
        {
            // Save answer
            await _articulationService.Answer(_articulationTestAnswer, answer);

            if (!ShowNextTest())
            {
                // Test ended open dialog box
                await OpenResultsDialog();
            }
        }

        private async Task OpenResultsDialog()
        {
            string result = await DialogService.SelectActionAsync("Select view",
                    "Select view", "Cancel", new string[] { "Phonological test results", "Bell curve chart" });
            DialogService.ShowLoading("Loading…");
            switch (result)
            {
                case "Phonological test results":
                    // Navigate to phonological test results
                    await NavigationService.NavigateToAsync<PhonologicalTestResultsViewModel>(_articulationTestAnswers);
                    await NavigationService.RemoveLastFromBackStackAsync();
                    break;
                case "Bell curve chart":
                    // Navigate to bell curve chart
                    await NavigationService.NavigateToAsync<BellCurveChartViewModel>(_articulationTestAnswers);
                    await NavigationService.RemoveLastFromBackStackAsync();
                    break;
                case "Cancel":
                    await NavigationService.NavigateBackAsync();
                    break;
            }
            DialogService.HideLoading();
        }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null && 
                Enum.TryParse<SoundPosition>(navigationData.ToString(), out var soundPosition))
            {
                await LoadData(soundPosition);
            }
        }
        private async Task LoadData(SoundPosition soundPosition)
        { 
            // Create new test
            _articulationTestAnswers = await _articulationService.GenerateTest(soundPosition);
            _articulationTestAnswersEnumerator = _articulationTestAnswers.GetEnumerator();

            TestCount = _articulationTestAnswers.Count();

            ShowNextTest();
        }

        private bool ShowNextTest()
        {
            var moved = _articulationTestAnswersEnumerator.MoveNext();
            if (moved)
            {
                _articulationTestAnswer = _articulationTestAnswersEnumerator.Current;

                TestIndex = Convert.ToString(_articulationTestAnswers.IndexOf(_articulationTestAnswersEnumerator.Current) + 1);
                Text = _articulationTestAnswer.ArticulationTest.Text;
                //ImageSource = ImageSource.FromResource("SpeechPathology.Assets.Images." + _articulationTestAnswer.ArticulationTest.Image);
                Image = "resource://SpeechPathology.Assets.Images." + _articulationTestAnswer.ArticulationTest.Image;
            }
            return moved;
        }
    }
}
