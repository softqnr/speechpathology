using CommonServiceLocator;
using DLToolkit.Forms.Controls;
using Plugin.Multilingual;
using SpeechPathology.Data;
using SpeechPathology.Infrastructure.Dialog;
using SpeechPathology.Infrastructure.Navigation;
using SpeechPathology.Infrastructure.Sound;
using SpeechPathology.Interfaces;
using SpeechPathology.Models;
using SpeechPathology.Resources;
using SpeechPathology.Services.AgeCalculator;
using SpeechPathology.Services.Articulation;
using SpeechPathology.Services.Flashcard;
using SpeechPathology.Services.Setting;
using SpeechPathology.Services.Worksheet;
using SpeechPathology.ViewModels;
using SpeechPathology.Views;
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
        public static string DatabaseFilePath { get; private set; }
        public static string Language = "EN";
        public static bool Initialized = false;
        public readonly static INavigationService NavigationService = new NavigationService();

        public App()
        {
            InitializeComponent();
            FlowListView.Init();

            if (!Initialized)
            {
                // Init DB
                InitializeDatabase();
                // Init DI
                InitializeDI();
                // Localization
                var task = InitializeLocalization();
                task.Wait(10);

                Initialized = true;
            }
            // Nav
            MainPage = new MasterDetailView();
        }

        private async Task InitializeLocalization()
        {
            ISettingService settingService  = ServiceLocator.Current.GetInstance<ISettingService>();
            Language = await settingService.GetByName("language");
            CrossMultilingual.Current.CurrentCultureInfo = new System.Globalization.CultureInfo(Language);
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
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
            NavigationService.Configure(typeof(AgeCalcPdfViewerViewModel), typeof(AgeCalcPdfViewerView));
            NavigationService.Configure(typeof(AgeCalcSpeechSoundsViewModel), typeof(AgeCalcSpeechSoundsView));

            await NavigationService.InitializeAsync();
        }

        private void InitializeDatabase()
        {
            DatabaseFilePath = DependencyService.Get<IFileAccessHelper>().GetDBPathAndCreateIfNotExists("sp.db");
        }

        private void InitializeDI()
        {
            Container = new UnityContainer();
            // Data repositories
            Container.RegisterType<IRepository<Setting>, Repository<Setting>>(new InjectionConstructor(DatabaseFilePath));
            Container.RegisterType<IRepository<ArticulationTest>, Repository<ArticulationTest>>(new InjectionConstructor(DatabaseFilePath));
            Container.RegisterType<IRepository<ArticulationTestExam>, Repository<ArticulationTestExam>>(new InjectionConstructor(DatabaseFilePath));
            Container.RegisterType<IRepository<ArticulationTestExamAnswer>, Repository<ArticulationTestExamAnswer>>(new InjectionConstructor(DatabaseFilePath));
            Container.RegisterType<IRepository<Flashcard>, Repository<Flashcard>>(new InjectionConstructor(DatabaseFilePath));
            Container.RegisterType<IRepository<Worksheet>, Repository<Worksheet>>(new InjectionConstructor(DatabaseFilePath));
            Container.RegisterType<IRepository<AgeCalculation>, Repository<AgeCalculation>>(new InjectionConstructor(DatabaseFilePath));

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
            Container.RegisterType<ISettingService, SettingService>();

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
            Container.RegisterType<AgeCalcPdfViewerViewModel>();
            Container.RegisterType<AgeCalcSpeechSoundsViewModel>();

            // Set as service locator provider
            var unityServiceLocator = new UnityServiceLocator(Container);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            // Navigation
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
