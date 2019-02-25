using Xamarin.Forms;

namespace SpeechPathology.Converters
{
    public sealed class BoolToColorConverter : BoolConverter<Color>
    {
        public BoolToColorConverter() :
            base(Color.FromHex("#00796B"), Color.FromHex("#EF3D60"))
        { }
    }
}
