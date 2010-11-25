using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace ReversiWpf
{
    public class CellPosConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            var posIdx = (System.Drawing.Point)value;
            int x = posIdx.X * 40;
            int y = posIdx.Y * 40;
            return new Thickness(x, y, 0, 0);
        }

        public object ConvertBack(object value, System.Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            var margine = (Thickness)value;
            int x = (int)margine.Left / 40;
            int y = (int)margine.Top / 40;
            return new Point(x, y);
        }

    }
}