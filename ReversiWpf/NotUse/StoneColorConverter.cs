using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ReversiWpf
{
    //public class StoneColorConverter : IValueConverter
    //{
    //    public object Convert(object value, System.Type targetType,
    //        object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        var state = (CellState)value;
    //        switch (state)
    //        {
    //            case CellState.Black:
    //                return new ImageBrush(new BitmapImage(
    //                    new Uri("Image/StoneBlack1.png", UriKind.Relative)));
    //                    //new Uri("Resources/BlackStone.png", UriKind.Relative)));
    //            case CellState.White:
    //                return new ImageBrush(new BitmapImage(
    //                    new Uri("Image/StoneWhite1.png", UriKind.Relative)));
    //            default:
    //                return new SolidColorBrush(Colors.Transparent);
    //        }
    //    }

    //    public object ConvertBack(object value, System.Type targetType,
    //        object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        //var brush = (Brush)value;
    //        return CellState.None;
    //    }

    //}
}
