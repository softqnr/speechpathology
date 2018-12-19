using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpeechPathology.Converters
{
    public class RandomNumberConverter : IValueConverter, IMarkupExtension
    {
        private Random _random = new Random();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result;
            Type parameterType = parameter.GetType();

            if (parameter != null && parameterType.IsArray)
            {
                int[] parameterArray = (int[])parameter;
                if (parameterArray.Length == 1)
                {
                    result = _random.Next(parameterArray[0]);
                }
                else { 
                    result = _random.Next(parameterArray[0], parameterArray[1]);
                }
            }
            else {
                result = _random.Next();
            }

            return result;
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
