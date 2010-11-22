using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReversiWpf
{
    /// <summary>
    /// Board.xaml の相互作用ロジック
    /// </summary>
    public partial class BoardView : UserControl
    {
        public BoardView()
        {
            InitializeComponent();
            RootCanvas.Background = new ImageBrush(new BitmapImage(
                        new Uri("Image/" + Option.Instance.BackImageFileName, 
                            UriKind.Relative)));
        }
    }
}
