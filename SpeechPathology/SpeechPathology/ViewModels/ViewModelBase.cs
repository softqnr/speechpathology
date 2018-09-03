using SpeechPathology.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpeechPathology.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        protected INavigationService _navigationService;
        //protected readonly IDialogService DialogService;
        public ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
                _isBusy = value;
                //RaisePropertyChanged(() => IsBusy);
                OnPropertyChanged("IsBusy");
            }
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
