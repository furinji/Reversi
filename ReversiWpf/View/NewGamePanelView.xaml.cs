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
    /// NewGamePanel.xaml の相互作用ロジック
    /// </summary>
    public partial class NewGamePanelView : UserControl
    {
        public NewGamePanelView()
        {
            InitializeComponent();
            image_Player1.Fill = ImageManager.GetBlackStoneImageBrush();
            image_Player2.Fill = ImageManager.GetWhiteStoneImageBrush();
        }
    }
}
