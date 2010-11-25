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
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var gamePanel = new GamePanel();
            Grid1.Children.Add(gamePanel);
            Board = new BoardViewModel(new BoardView());
            Board.BoardView.DataContext = Board;
            gamePanel.Grid_Board.Children.Add(Board.BoardView);
            InfoPanel = new InfoPanelViewModel(new InfoPanelView());
            InfoPanel.InterfacePanelView.DataContext = InfoPanel;
            gamePanel.Grid_Ifpanel.Children.Add(InfoPanel.InterfacePanelView);
            this.Closed += new EventHandler(MainWindow_Closed);
            var newGamePanel = new NewGamePanelView();
            Grid1.Children.Add(newGamePanel);
            NewGamePanel = new NewGamePanelViewModel(newGamePanel);
            Game.Instance.NewGame("PauseAutoLoop");
        }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            //Option.Instance.SaveToFile();
        }

        public static InfoPanelViewModel InfoPanel { get; private set; }
        public static BoardViewModel Board { get; private set; }
        public static NewGamePanelViewModel NewGamePanel { get; private set; }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            Game.Instance.NewGame("PauseAutoLoop");
        }
    }
}
