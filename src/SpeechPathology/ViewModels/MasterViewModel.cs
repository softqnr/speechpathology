using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechPathology.ViewModels
{
    public class MasterViewModel : ViewModelBase
    {
        public string ProjectTitle
        {
            get => Resources.AppResources.ProjectTitle;
        }

        public string Version
        {
            get => DependencyService.Get<Interfaces.IAppVersionProvider>().AppVersion;
        }

        public string Language {
            get => $"-{App.Language}";
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public ICommand NavigationItemSelectedCommand
        {
            get
            {
                return new Command(async (sender) =>
                {
                    await NavigateTo(sender);
                });
            }
        }
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

        public ObservableCollection<NavMenuItem> MenuItems { get; set; }

        public MasterViewModel()
        {
            MenuItems = new ObservableCollection<NavMenuItem>(new[]
            {
                    new NavMenuItem { Id = 0, Title = Resources.AppResources.Disclaimer, TargetType=typeof(AboutViewModel) },
                    new NavMenuItem { Id = 1, Title = Resources.AppResources.RecomendedUsage, Parameter = Resources.AppResources.RecomendedUsage + "|http://www.speechpathologytools.eu/recommended_usage.html", TargetType=typeof(WebViewerViewModel) },
            });
        }

        public async Task NavigateTo(object sender)
        {
            var item = sender as NavMenuItem;
            if (item == null)
                return;

            Title = item.Title;
            if (item.Parameter != null)
            {
                await NavigationService.NavigateToAsync(item.TargetType, item.Parameter);
            }
            else
            {
                await NavigationService.NavigateToAsync(item.TargetType);
            }
        }
    }
    public class NavMenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Type TargetType { get; set; }
        public object Parameter { get; set; }
    }
}
