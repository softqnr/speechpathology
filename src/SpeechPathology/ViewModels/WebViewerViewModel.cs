using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class WebViewerViewModel : ViewModelBase
    {
        private string _title;
        private string _url;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }
        public ICommand OpenInChromeCommand
        {
            get
            {
                return new Command(() =>
                {
                    Device.OpenUri(new Uri(Url));
                });
            }
        }

        public WebViewerViewModel()
        { 
        }

        public override async Task InitializeAsync(object navigationData)
        {
           if (navigationData != null)
            {
                var paramArray = navigationData.ToString().Split('|');
                Title = paramArray[0];
                Url = paramArray[1];
            }
            await Task.FromResult(true);
        }
    }
}
