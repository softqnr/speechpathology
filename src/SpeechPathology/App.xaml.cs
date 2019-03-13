using CommonServiceLocator;
using DLToolkit.Forms.Controls;
using Plugin.Multilingual;
using SpeechPathology.Data;
using SpeechPathology.Services.AgeCalculator;
using SpeechPathology.Services.Articulation;
using SpeechPathology.Services.Flashcard;
using SpeechPathology.Services.Worksheet;
using SpeechPathology.Interfaces;
using SpeechPathology.Models;
using SpeechPathology.Resources;
using SpeechPathology.Infrastructure.Dialog;
using SpeechPathology.Infrastructure.Navigation;
using SpeechPathology.Infrastructure.PDF;
using SpeechPathology.Infrastructure.Sound;
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
        public static MasterDetailPage MasterPage { get; set; }
        public static UnityContainer Container { get; private set; }
        public string DatabaseFilePath { get; private set; }
        public readonly static INavigationService NavigationService = new NavigationService();
        public App()
        {
            InitializeComponent();
            FlowListView.Init();
            // Localization
            AppResources.Culture = CrossMultilingual.Current.DeviceCultureInfo;
            // Init DB
            InitializeDatabase();
            // Init DI
            InitializeDI();

            // Nav
            MainPage = new MasterDetailView();
        }

        private async Task InitializeNavigation()
        {
            NavigationService.Configure(typeof(MasterViewModel), typeof(MasterView));
            NavigationService.Configure(typeof(MainViewModel), typeof(MainView));
            NavigationService.Configure(typeof(ArticulationTestViewModel), typeof(ArticulationTestView));
            NavigationService.Configure(typeof(PositionTestResultsViewModel), typeof(PositionTestResultsView));
            NavigationService.Configure(typeof(SoundTestResultsViewModel), typeof(SoundTestResultsView));
            NavigationService.Configure(typeof(AgeCalculatorViewModel), typeof(AgeCalculatorView));
            NavigationService.Configure(typeof(FlashcardsSelectSoundViewModel), typeof(FlashcardsSelectSoundView));
            NavigationService.Configure(typeof(FlashcardsSelectSoundPositionViewModel), typeof(FlashcardsSelectSoundPositionView)); 
            NavigationService.Configure(typeof(FlashcardsTestViewModel), typeof(FlashcardsTestView));
            NavigationService.Configure(typeof(WorksheetsViewModel), typeof(WorksheetsView));
            NavigationService.Configure(typeof(PdfViewerViewModel), typeof(PdfViewerView));
            NavigationService.Configure(typeof(WebViewerViewModel), typeof(WebViewerView));
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
            // Data repositories
            Container.RegisterType<IRepository<ArticulationTest>, Repository<ArticulationTest>>(new InjectionConstructor(DatabaseFilePath));
            Container.RegisterType<IRepository<ArticulationTestExam>, Repository<ArticulationTestExam>>(new InjectionConstructor(DatabaseFilePath));
            Container.RegisterType<IRepository<ArticulationTestExamAnswer>, Repository<ArticulationTestExamAnswer>>(new InjectionConstructor(DatabaseFilePath));
            Container.RegisterType<IRepository<Flashcard>, Repository<Flashcard>>(new InjectionConstructor(DatabaseFilePath));
            Container.RegisterType<IRepository<Worksheet>, Repository<Worksheet>>(new InjectionConstructor(DatabaseFilePath));
            Container.RegisterType<IRepository<AgeCalculation>, Repository<AgeCalculation>>(new InjectionConstructor(DatabaseFilePath));
            //Container.RegisterInstance(new AgeCalculation(), new ContainerControlledLifetimeManager());

            // Infrastructure
            Container.RegisterInstance(NavigationService, new ContainerControlledLifetimeManager());
            Container.RegisterType<ISoundService, SoundService>();
            Container.RegisterType<IDialogService, DialogService>();
            
            // Services
            Container.RegisterType<IArticulationTestService, ArticulationTestService>();
            Container.RegisterType<IPDFGeneratorService, PDFGeneratorService>();
            Container.RegisterType<IFlashcardService, FlashcardService>();
            Container.RegisterType<IWorksheetService, WorksheetService>();
            Container.RegisterType<IAgeCalculatorService, AgeCalculatorService>();

            // View models
            Container.RegisterType<MainViewModel>();
            Container.RegisterType<ArticulationTestViewModel>();
            Container.RegisterType<PositionTestResultsViewModel>();
            Container.RegisterType<SoundTestResultsViewModel>();
            Container.RegisterType<AgeCalculatorViewModel>();
            Container.RegisterType<FlashcardsSelectSoundViewModel>();
            Container.RegisterType<FlashcardsSelectSoundPositionViewModel>();
            Container.RegisterType<FlashcardsTestViewModel>();
            Container.RegisterType<WorksheetsViewModel>();
            Container.RegisterType<PdfViewerViewModel>();
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
