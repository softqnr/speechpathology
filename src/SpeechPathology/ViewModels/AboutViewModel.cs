using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        public ICommand LinkTappedCommand
        {
            get
            {
                return new Command<string>((s) =>
                {
                    OnLinkClicked(s);
                });
            }
        }
        public void OnLinkClicked(string url)
        {
            Device.OpenUri(new Uri(url));
        }

    }
}
