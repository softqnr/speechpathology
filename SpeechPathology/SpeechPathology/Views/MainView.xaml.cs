using SpeechPathology.Interfaces;
using SpeechPathology.Services.Navigation;
using System;
using Xamarin.Forms;

namespace SpeechPathology.Views
{
    public partial class MainView : ContentPage
    {
        private INavigationService NavigationService { get; } = App.NavigationService;
        public MainView()
        {
            InitializeComponent();
        }

        private void AgeCalculatorButton_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IMessage>().ShortAlert("Age Calculator");
        }

        private void FlashcardsButton_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IMessage>().ShortAlert("Flash Cards");
        }

        private void WorksheetsButton_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IMessage>().ShortAlert("Worksheets");
        }

        private void OnNavigateBack(object sender, EventArgs e)
        {
            NavigationService.NavigateBackAsync();
        }
    }
}
