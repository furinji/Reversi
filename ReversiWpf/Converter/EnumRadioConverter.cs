using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ReversiWpf
{
    public class EnumRadioConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var paramString = parameter as string;
            if (paramString == null)
            {
                return System.Windows.DependencyProperty.UnsetValue;
            }

            if (!Enum.IsDefined(value.GetType(), paramString))
            {
                return System.Windows.DependencyProperty.UnsetValue;
            }

            var paramParsed = Enum.Parse(value.GetType(), paramString);

            return (value.Equals(paramParsed));
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var paramString = parameter as string;
            if (paramString == null)
            {
                return System.Windows.DependencyProperty.UnsetValue;
            }

            var isChecked = (bool)value;
            if (!isChecked)
            {
                return System.Windows.DependencyProperty.UnsetValue;
            }

            return Enum.Parse(targetType, paramString);
        }

    }
}