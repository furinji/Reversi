using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ReversiWpf
{
    public class ImageManager
    {
        public static Brush GetBlackStoneImageBrush()
        {
            return new ImageBrush(new BitmapImage(
                new Uri("Image/" + Option.Instance.BlackStoneImageFileName, UriKind.Relative)));
        }
        public static Brush GetWhiteStoneImageBrush()
        {
            return new ImageBrush(new BitmapImage(
                new Uri("Image/" + Option.Instance.WhiteStoneImageFileName, UriKind.Relative)));
        }

        public static Brush GetStoneImageBrush(CellState stoneColor)
        {
            if (stoneColor == CellState.Black)
            {
                return GetBlackStoneImageBrush();
            }
            else
            {
                return GetWhiteStoneImageBrush();
            }
        }

    }
}
