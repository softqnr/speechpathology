﻿using CommonServiceLocator;
using Plugin.Multilingual;
using SpeechPathology.Data;
using SpeechPathology.DataServices.Articulation;
using SpeechPathology.Interfaces;
using SpeechPathology.Models;
using SpeechPathology.Resources;
using SpeechPathology.Services.Dialog;
using SpeechPathology.Services.Navigation;
using SpeechPathology.ViewModels;
using SpeechPathology.Views;
using SQLite;
using Unity;
using Unity.Lifetime;
using Unity.ServiceLocation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SpeechPathology
{
    public partial class App : Application
    {
        public static UnityContainer Container { get; private set; }
        public static SQLiteAsyncConnection SQLiteConnection { get; private set; }
        public readonly static INavigationService NavigationService = new NavigationService();
        public static DatabaseContext DatabaseContext { get; protected set; }
        public static string DatabasePath { get; private set; }
        public App()
        {
            InitializeComponent();
            // Localization
            AppResources.Culture = CrossMultilingual.Current.DeviceCultureInfo;
            // Init DB
            InitializeDatabase();
            // Init IOC
            InitializeIOC();
            // Nav service configuration
            InitializeNavigation();
        }

        private void InitializeNavigation()
        {
            NavigationService.Configure(typeof(MainViewModel), typeof(MainView));
            NavigationService.Configure(typeof(ArticulationTestViewModel), typeof(ArticulationTestView));
            NavigationService.Configure(typeof(PhonologicalTestResultsViewModel), typeof(PhonologicalTestResultsView));
            NavigationService.Configure(typeof(SelectSoundLocationViewModel), typeof(SelectSoundLocationView));
            NavigationService.Configure(typeof(AgeCalculatorViewModel), typeof(AgeCalculatorView));
            NavigationService.Configure(typeof(FlashcardsViewModel), typeof(FlashcardsView));
            NavigationService.Configure(typeof(WorksheetsViewModel), typeof(WorksheetsView));
            NavigationService.Configure(typeof(AboutViewModel), typeof(AboutView));
            NavigationService.InitializeAsync();
        }

        private async void InitializeDatabase()
        {
            string databasePath = await DependencyService.Get<IFileAccessHelper>().GetDBPathAndCreateIfNotExists("db_en.db");
            SQLiteConnection = new SQLiteAsyncConnection(databasePath);
        }

        private void InitializeIOC()
        {
            App.Container = new UnityContainer();
            // Data
            Container.RegisterInstance(SQLiteConnection, new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<ArticulationTest>, Repository<ArticulationTest>>();
            Container.RegisterType<IRepository<ArticulationTestAnswer>, Repository<ArticulationTestAnswer>>();
            // Services
            Container.RegisterInstance(NavigationService, new ContainerControlledLifetimeManager());
            Container.RegisterType<IDialogService, DialogService>();
            
            // Data services
            Container.RegisterType<IArticulationService, ArticulationService>();

            // View models
            Container.RegisterType<MainViewModel>();
            Container.RegisterType<ArticulationTestViewModel>();
            Container.RegisterType<PhonologicalTestResultsViewModel>();
            Container.RegisterType<SelectSoundLocationViewModel>();
            Container.RegisterType<AgeCalculatorViewModel>();
            Container.RegisterType<FlashcardsViewModel>();
            Container.RegisterType<WorksheetsViewModel>();
            Container.RegisterType<AboutViewModel>();

            // Set as service locator provider
            var unityServiceLocator = new UnityServiceLocator(Container);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
