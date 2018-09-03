using SpeechPathology.Models;
using SpeechPathology.Services.Articulation;
using SpeechPathology.Services.Navigation;
using SpeechPathology.Utils;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using System.IO;

namespace SpeechPathology.ViewModels
{
    public class ArticulationTestViewModel : ViewModelBase
    {
        private IArticulationService _articulationService;
        private IList<ArticulationTestAnswer> _articulationTestAnswers;
        private IEnumerator<ArticulationTestAnswer> _articulationTestAnswersEnumerator;
        private ArticulationTestAnswer _articulationTestAnswer;

        private string _testIndex;
        private string _text;
        private ImageSource _imageSource;
        private string _image;

        public ICommand AnswerCommand { get; private set; } //=> new AsyncCommand(AnswerAsync);

        public string TestIndex
        {
            get
            {
                return _testIndex;
            }
            set
            {
                _testIndex = value;
                OnPropertyChanged("TestIndex");
            }
        }

        public string Text
        {
            get {
                return _text;
            }
            set {
                _text = value;
                OnPropertyChanged("Text");
            }
        }

        public ImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }


        public ArticulationTestViewModel(INavigationService navigationService, IArticulationService articulationService) : base(navigationService)
        {
            // 
            _articulationService = articulationService;
            AnswerCommand = new Command<bool>(async (b) => await AnswerAsync(b)); 
        }

        private async Task AnswerAsync(bool answer)
        {
            // Save answer
            await _articulationService.Answer(_articulationTestAnswer, answer);

            if (!NextTest(true))
            {
                // Test ended navigate to results
                await _navigationService.NavigateToAsync<PhonologicalTestResultsViewModel>(_articulationTestAnswers);
                await _navigationService.RemoveLastFromBackStackAsync();
            }

            //return Task.FromResult(true);
        }


        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData == null)
            {
                // Resume
                _articulationTestAnswers = await _articulationService.GenerateTest((string)navigationData);
                //_articulationTestAnswersEnumerator = _articulationTestAnswers.GetEnumerator();
            } else { 
                // Create new
                _articulationTestAnswers = await _articulationService.GenerateTest((string)navigationData);
                _articulationTestAnswersEnumerator = _articulationTestAnswers.GetEnumerator();
            }
            NextTest(true);
        }

        private bool NextTest(bool forward)
        {
            var moved = _articulationTestAnswersEnumerator.MoveNext();
            if (moved)
            {
                _articulationTestAnswer = _articulationTestAnswersEnumerator.Current;
                TestIndex = Convert.ToString(_articulationTestAnswers.IndexOf(_articulationTestAnswersEnumerator.Current) + 1) + " of "
                    + _articulationTestAnswers.Count().ToString();
                Text = _articulationTestAnswer.ArticulationTest.Text;
                //ImageSource = ImageSource.FromResource("SpeechPathology.Assets.Images." + _articulationTestAnswer.ArticulationTest.Image);
                Image = "resource://SpeechPathology.Assets.Images." + _articulationTestAnswer.ArticulationTest.Image;

                OnPropertyChanged("TestIndex");
            }
            return moved;
        }
    }
}
