using SpeechPathology.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using SpeechPathology.Services.Articulation;
using SpeechPathology.Models.Enums;

namespace SpeechPathology.ViewModels
{
    public class ArticulationTestViewModel : ViewModelBase
    {
        private IArticulationTestService _articulationTestService;
        private ArticulationTestExam _articulationTestExam;
        private IEnumerator<ArticulationTestExamAnswer> _articulationTestAnswersEnumerator;
        private ArticulationTestExamAnswer _articulationTestAnswer;

        private string _testIndex;
        private int? _testCount;
        private string _text;
        private string _image;

        public ICommand AnswerTestCommand { get; private set; } //=> new AsyncCommand(AnswerAsync);

        public string TestIndex
        {
            get => _testIndex;
            set => SetProperty(ref _testIndex, value);
        }

        public int? TestCount
        {
            get => _testCount;
            set => SetProperty(ref _testCount, value);
        }

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public string Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public ArticulationTestViewModel(IArticulationTestService articulationTestService) 
        {
            _articulationTestService = articulationTestService;
            AnswerTestCommand = new Command<bool>(async (b) => await AnswerTestAsync(b)); 
        }

        private async Task AnswerTestAsync(bool answer)
        {
            // Save answer
            await _articulationTestService.Answer(_articulationTestAnswer, answer);
            
            // Test ended
            if (!ShowNextTest())
            {
                // Close exam
                _articulationTestExam = await _articulationTestService.CloseExam(_articulationTestExam);
                // Open dialog box
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
                    await NavigationService.NavigateToAsync<PhonologicalTestResultsViewModel>(_articulationTestExam);
                    await NavigationService.RemoveLastFromBackStackAsync();
                    break;
                case "Bell curve chart":
                    // Navigate to bell curve chart
                    await NavigationService.NavigateToAsync<BellCurveChartViewModel>(_articulationTestExam);
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
            _articulationTestExam = await _articulationTestService.GenerateExam(soundPosition);
            _articulationTestAnswersEnumerator = _articulationTestExam.Answers.GetEnumerator();

            TestCount = _articulationTestExam.Answers.Count();

            ShowNextTest();
        }

        private bool ShowNextTest()
        {
            var moved = _articulationTestAnswersEnumerator.MoveNext();
            if (moved)
            {
                _articulationTestAnswer = _articulationTestAnswersEnumerator.Current;

                TestIndex = _articulationTestAnswer.Number.ToString();
                Text = _articulationTestAnswer.ArticulationTest.Text;
                Image = _articulationTestAnswer.ArticulationTest.Image;
            }
            return moved;
        }
    }
}
