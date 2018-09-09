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
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
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
        public string DatabaseFilePath { get; private set; }
        public readonly static INavigationService NavigationService = new NavigationService();
        public App()
        {
            InitializeComponent();
            // Localization
            AppResources.Culture = CrossMultilingual.Current.DeviceCultureInfo;
            // Init DB
            InitializeDatabase();
            // Init DI
            InitializeDI();
        }

        private async Task InitializeNavigation()
        {
            NavigationService.Configure(typeof(MainViewModel), typeof(MainView));
            NavigationService.Configure(typeof(ArticulationTestViewModel), typeof(ArticulationTestView));
            NavigationService.Configure(typeof(PhonologicalTestResultsViewModel), typeof(PhonologicalTestResultsView));
            NavigationService.Configure(typeof(BellCurveChartViewModel), typeof(BellCurveChartView));
            NavigationService.Configure(typeof(AgeCalculatorViewModel), typeof(AgeCalculatorView));
            NavigationService.Configure(typeof(FlashcardsViewModel), typeof(FlashcardsView));
            NavigationService.Configure(typeof(WorksheetsViewModel), typeof(WorksheetsView));
            NavigationService.Configure(typeof(AboutViewModel), typeof(AboutView));
            await NavigationService.InitializeAsync();
        }

        private void InitializeDatabase()
        {
            DatabaseFilePath = DependencyService.Get<IFileAccessHelper>().GetDBPathAndCreateIfNotExists("db_en.db");
        }

        private void InitializeDI()
        {
            Container = new UnityContainer();
            // Data
            Container.RegisterType<IRepository<ArticulationTest>, Repository<ArticulationTest>>(new InjectionConstructor(DatabaseFilePath));
            Container.RegisterType<IRepository<ArticulationTestAnswer>, Repository<ArticulationTestAnswer>>(new InjectionConstructor(DatabaseFilePath));
            // Services
            Container.RegisterInstance(NavigationService, new ContainerControlledLifetimeManager());
            Container.RegisterType<IDialogService, DialogService>();
            
            // Data services
            Container.RegisterType<IArticulationService, ArticulationService>();

            // View models
            Container.RegisterType<MainViewModel>();
            Container.RegisterType<ArticulationTestViewModel>();
            Container.RegisterType<PhonologicalTestResultsViewModel>();
            Container.RegisterType<BellCurveChartViewModel>();
            Container.RegisterType<AgeCalculatorViewModel>();
            Container.RegisterType<FlashcardsViewModel>();
            Container.RegisterType<WorksheetsViewModel>();
            Container.RegisterType<AboutViewModel>();

            // Set as service locator provider
            var unityServiceLocator = new UnityServiceLocator(Container);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            // Nav service configuration
            await InitializeNavigation();
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
