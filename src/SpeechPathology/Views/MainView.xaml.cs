using Xamarin.Forms;

namespace SpeechPathology.Views
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();

            Grid.SetColumnSpan(AgeCalculatorButton, 2);
            var grid = ArticulationButton.Parent as Grid;
            grid.Children.Remove(ArticulationButton);
        }
    }
}
