﻿using CommonServiceLocator;
using SpeechPathology.ViewModels;
using SpeechPathology.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SpeechPathology.Infrastructure.Navigation
{
    public partial class NavigationService : INavigationService
    {
        private readonly object _sync = new object();

        protected readonly Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        public NavigationService()
        {
            
        }

        public async Task InitializeAsync()
        {
            //if (await _authenticationService.UserIsAuthenticatedAndValidAsync())
            //{
            await NavigateToAsync<MainViewModel>();
            //}
            //else
            //{
            ////await NavigateToAsync<LoginViewModel>();
            //}
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage is MasterDetailPage)
            {
                var mainPage = CurrentApplication.MainPage as MasterDetailPage;
                await mainPage.Detail.Navigation.PopAsync();
            }
            else if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {   
            if (CurrentApplication.MainPage is NavigationPage)
            {
                var mainPage = CurrentApplication.MainPage as NavigationPage;
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            } else if (CurrentApplication.MainPage is MasterDetailPage) {
                // TODO: Check this if it works properly
                var mainPage = CurrentApplication.MainPage as MasterDetailPage;
                mainPage.Detail.Navigation.RemovePage(
                    mainPage.Detail.Navigation.NavigationStack[mainPage.Detail.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            var page = CreateAndBindPage(viewModelType, parameter);

            if (page is MasterDetailView && page is ContentPage)
            {
                CurrentApplication.MainPage = new CustomNavigationPage(page);
            }
            //else if (page is LoginView)
            //{
            //    CurrentApplication.MainPage = new CustomNavigationPage(page);
            //}
            else if (CurrentApplication.MainPage is MasterDetailPage)
            {
                var mainPage = CurrentApplication.MainPage as MasterDetailPage;

                if (mainPage.Detail is CustomNavigationPage navigationPage)
                {
                    //var currentPage = navigationPage.CurrentPage;
                    //
                    //if (currentPage.GetType() != page.GetType())
                    //{
                    await navigationPage.PushAsync(page);
                    //}
                }
                else
                {
                    navigationPage = new CustomNavigationPage(page);
                    mainPage.Detail = navigationPage;
                }

                mainPage.IsPresented = false;
            }
            else
            {
                if (CurrentApplication.MainPage is CustomNavigationPage navigationPage)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = new CustomNavigationPage(page);
                }
            }
            await (page.BindingContext as ViewModelBase)?.InitializeAsync(parameter);
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            //ViewModelBase viewModel = Locator.Instance.Resolve(viewModelType) as ViewModelBase;
            ViewModelBase viewModel = ServiceLocator.Current.GetInstance(viewModelType) as ViewModelBase;
            page.BindingContext = viewModel;
            
            return page;
        }

        protected virtual void SetMainPage(Page page)
        {
            if (CurrentApplication.MainPage != null)
            {
                //var viewModel = CurrentApplication.MainPage.BindingContext as ViewModelBase;
                //viewModel?.Dispose();
            }

            CurrentApplication.MainPage = page;
        }
        public void Configure(Type viewModel, Type view)
        {
            lock (_sync)
            {
                if (_mappings.ContainsKey(viewModel))
                {
                    _mappings[viewModel] = view;
                }
                else
                {
                    _mappings.Add(viewModel, view);
                }
            }
        }

    }
}