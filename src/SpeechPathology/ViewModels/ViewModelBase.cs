using CommonServiceLocator;
using SpeechPathology.Infrastructure.Dialog;
using SpeechPathology.Infrastructure.Navigation;
using System.Threading.Tasks;

namespace SpeechPathology.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        protected readonly INavigationService NavigationService;
        protected readonly IDialogService DialogService;

        public ViewModelBase()
        {
            DialogService = ServiceLocator.Current.GetInstance<IDialogService>();
            NavigationService = ServiceLocator.Current.GetInstance<INavigationService>();
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                SetProperty(ref _isBusy, value);
            }
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
