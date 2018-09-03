using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace SpeechPathology.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectSoundLocationView : PopupPage
	{
		public SelectSoundLocationView ()
		{
			InitializeComponent ();
		}
        protected override bool OnBackgroundClicked()
        {
            return false;
        }
    }
}