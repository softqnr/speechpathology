using SpeechPathology.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpeechPathology.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterView : ContentPage
    {
        public MasterView()
        {
            InitializeComponent();
            // TODO: Change this 
            BindingContext = new MasterViewModel();
        }
    }
}