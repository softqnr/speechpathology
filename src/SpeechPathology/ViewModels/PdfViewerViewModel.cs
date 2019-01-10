using System.Threading.Tasks;

namespace SpeechPathology.ViewModels
{
    public class PdfViewerViewModel : ViewModelBase
    {
        private string _pdfFile;

        public string PdfFile
        {
            get => _pdfFile;
            set => SetProperty(ref _pdfFile, value);
        }

        public PdfViewerViewModel()
        {
        }

        public override async Task InitializeAsync(object navigationData)
        {
           if (navigationData != null)
            {
                PdfFile = (string)navigationData;
            }
            await Task.FromResult(true);
        }
    }
}
