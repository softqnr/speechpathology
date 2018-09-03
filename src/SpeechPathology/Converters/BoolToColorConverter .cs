using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpeechPathology.Converters
{
    public class BoolToColorConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var valueAsBool = value as bool?;
            switch (valueAsBool)
            {
                case (true):
                    {
                        return Color.FromHex("#00796B");
                    }
                default:
                    {
                        return Color.FromHex("#EF3D60");
                    }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Only one way bindings are supported with this converter");
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

    }
}
