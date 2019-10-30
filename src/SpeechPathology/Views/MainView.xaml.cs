using Xamarin.Forms;

namespace SpeechPathology.Views
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();

            if (App.Language == "EN" || App.Language == "SI")
            {
                Grid.SetColumnSpan(AgeCalculatorButton, 2);
                var grid = ArticulationButton.Parent as Grid;
                grid.Children.Remove(ArticulationButton);
            }
        }
    }
}
