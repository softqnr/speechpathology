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
        private bool _testViewIsVisible = true;

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

        public bool TestViewIsVisible
        {
            get => _testViewIsVisible;
            set => SetProperty(ref _testViewIsVisible, value);
        }

        public ICommand OpenSoundTestResultsCommand
        {
            get
            {
                return new Command<string>(async (s) =>
                {
                    // Navigate to sound test results
                    await NavigationService.NavigateToAsync<SoundTestResultsViewModel>(_articulationTestExam);
                });
            }
        }

        public ICommand OpenPositionTestResultsCommand
        {
            get
            {
                return new Command<string>(async (s) =>
                {
                    // Navigate to position test results
                    await NavigationService.NavigateToAsync<PositionTestResultsViewModel>(_articulationTestExam);
                });
            }
        }

        public ICommand CancelTestResultsCommand
        {
            get
            {
                return new Command<string>(async (s) =>
                {
                    // Back
                    await NavigationService.NavigateBackAsync();
                });
            }
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
                // Open dialog view
                await OpenResultsView();
            }
        }

        private async Task OpenResultsView()
        {
            DialogService.ShowLoading(Resources.AppResources.Loading);
            await Task.Delay(500);
            TestViewIsVisible = false;
            DialogService.HideLoading();
        }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null && navigationData is SoundPosition)
            {
                if (Enum.TryParse<SoundPosition>(navigationData.ToString(), out var soundPosition))
                {
                    await LoadData(soundPosition);
                }
            }
            else if (navigationData != null && navigationData is int)
            {
                await LoadData((int)navigationData);
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
        private async Task LoadData(int age)
        {
            _articulationTestExam = await _articulationTestService.GenerateExam(age);
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
