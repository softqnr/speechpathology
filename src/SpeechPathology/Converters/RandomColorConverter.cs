using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpeechPathology.Converters
{
    public class RandomColorConverter : IValueConverter, IMarkupExtension
    {
        private Random _random = new Random();
        private List<string> _colors = new List<string>() {
            "0047BD",
            "09B9FC",
            "009543",
            "00AB3B",
            "FFB300",
            "FFCE00",
            "EA0034",
            "FF822A",
            "8200AC",
            "B610BF"};

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var stringValue = value as string;

            string color = _colors[_random.Next(_colors.Count)];
        
            return Color.FromHex(color);
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
