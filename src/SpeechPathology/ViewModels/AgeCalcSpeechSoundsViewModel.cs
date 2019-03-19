using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class AgeCalcSpeechSoundsViewModel : ViewModelBase
    {
        private string _speechSoundsPath;

        public string SpeechSoundsPath
        {
            get => _speechSoundsPath;
            set
            {
                SetProperty(ref _speechSoundsPath, value);
                OnPropertyChanged(nameof(SpeechSoundsFile));
            }
        }

        public ImageSource SpeechSoundsFile
        {
            get
            {
                return ImageSource.FromFile(SpeechSoundsPath);
            }
        }

        public AgeCalcSpeechSoundsViewModel() { }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                string documentsPath = string.Empty;

                //documentsPath = "file:///android_asset/";
                //documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                //documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                SpeechSoundsPath = Path.Combine(documentsPath + (string)navigationData);
            }
            await Task.FromResult(true);
        }
    }
}
