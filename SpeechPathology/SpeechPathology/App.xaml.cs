using CommonServiceLocator;
using SpeechPathology.Data;
using SpeechPathology.Interfaces;
using SpeechPathology.Models;
using SpeechPathology.Services.Articulation;
using SpeechPathology.Services.Navigation;
using SpeechPathology.ViewModels;
using SpeechPathology.Views;
using System.ComponentModel;
using System.Threading.Tasks;
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
        public static UnityContainer Container { get; set; }
        public static NavigationService NavigationService { get; } = new NavigationService();
        public static DatabaseContext DatabaseContext { get; protected set; }
        public static string DatabasePath { get; private set; }
        public App()
        {
            InitializeComponent();
            // Init DB
            InitializeDatabase();
            // 
            InitializeIOC();
            // Nav service configuration
            InitializeNavigation();
        }

        private Task InitializeNavigation()
        {
            NavigationService.Configure(typeof(MainViewModel), typeof(MainView));
            NavigationService.Configure(typeof(ArticulationTestViewModel), typeof(ArticulationTestView));
            NavigationService.Configure(typeof(PhonologicalTestResultsViewModel), typeof(PhonologicalTestResultsView));
            NavigationService.Configure(typeof(SelectSoundLocationViewModel), typeof(SelectSoundLocationView));
            NavigationService.Configure(typeof(AgeCalculatorViewModel), typeof(AgeCalculatorView));
            NavigationService.Configure(typeof(FlashcardsViewModel), typeof(FlashcardsView));
            NavigationService.Configure(typeof(WorksheetsViewModel), typeof(WorksheetsView));
            NavigationService.Configure(typeof(AboutViewModel), typeof(AboutView));
            //var mainPage = ((NavigationService)NavigationService).SetRootPage(nameof(MainView));
            //MainPage = mainPage;

            return NavigationService.NavigateToAsync<MainViewModel>();
        }

        private async void InitializeDatabase()
        {
            DatabasePath = await DependencyService.Get<IFileAccessHelper>().GetDBPathAndCreateIfNotExists("db_en.db");
            //DatabaseContext = new DatabaseContext(databasePath);
        }

        private void InitializeIOC()
        {
            App.Container = new UnityContainer();
            // Services
            Container.RegisterInstance<INavigationService>(NavigationService, new ContainerControlledLifetimeManager());
            Container.RegisterType<IArticulationService, ArticulationService>();
            // View models
            Container.RegisterInstance(typeof(MainViewModel));
            Container.RegisterInstance(typeof(ArticulationTestViewModel));
            Container.RegisterInstance(typeof(PhonologicalTestResultsViewModel));
            Container.RegisterInstance(typeof(SelectSoundLocationViewModel));
            Container.RegisterInstance(typeof(AgeCalculatorViewModel));
            Container.RegisterInstance(typeof(FlashcardsViewModel));
            Container.RegisterInstance(typeof(WorksheetsViewModel));
            Container.RegisterInstance(typeof(AboutViewModel));

            // Set location provider
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
