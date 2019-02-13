using SpeechPathology.Models;
using SpeechPathology.Services.Worksheet;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class WorksheetsViewModel : ViewModelBase
    {
        private IWorksheetService _worksheetService;
        private List<Worksheet> _worksheets;
        private string _uri;

        public List<Worksheet> Worksheets
        {
            get => _worksheets;
            set => SetProperty(ref _worksheets, value);
        }
        public string Uri
        {
            get => _uri;
            set => SetProperty(ref _uri, value);
        }
        public ICommand ItemTappedCommand
        {
            get
            {
                return new Command<Worksheet>(async (w) =>
                {
                    await OnLetterSelected(w);
                });
            }
        }
        public WorksheetsViewModel(IWorksheetService worksheetService)
        {
            _worksheetService = worksheetService;
        }
        public override async Task InitializeAsync(object navigationData)
        {
            await LoadData();
        }
        private async Task LoadData()
        {
            // Get worksheets
            var worksheets = await _worksheetService.GetAllAsync();
            Worksheets = worksheets;
        }
        public async Task OnLetterSelected(Worksheet ws)
        {
            if (ws != null)
            {
                DialogService.ShowLoading(Resources.AppResources.Loading);
                await NavigationService.NavigateToAsync<PdfViewerViewModel>("Worksheets/" + ws.File);
                DialogService.HideLoading();
            }
        }
    }
}
