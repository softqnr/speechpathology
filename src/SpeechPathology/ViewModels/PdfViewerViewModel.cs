using SpeechPathology.Interfaces;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class PdfViewerViewModel : ViewModelBase
    {
        private string _pdfFile;

        public ICommand SavePdfCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await OnSavePdfClicked();
                });
            }
        }

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

        public async Task OnSavePdfClicked()
        {
            IsBusy = true;
            var filename = PdfFile.Substring(PdfFile.LastIndexOf('/') + 1);
            //var filename = "worksheet.pdf";
            var shareTitle = Resources.AppResources.ShareWorksheet;
            var shareMsg = Resources.AppResources.ShareWorksheet;
            string filepath = await DependencyService.Get<IFileAccessHelper>().CopyAssetFileToTemp(PdfFile, filename);
            DependencyService.Get<IShare>().ShareFile(shareTitle, shareMsg, filepath);
            IsBusy = false;
        }
    }
}
