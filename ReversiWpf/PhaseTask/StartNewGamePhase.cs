using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public class StartNewGamePhase : IPhaseTask
    {
        public StartNewGamePhase()
        {
            IsComplete = false;
        }
        public StartNewGamePhase(string param)
        {
            IsComplete = false;
            if (param == "PauseAutoLoop") { _isPauseAutoLoop = true; }
        }

        private bool _isPauseAutoLoop = false;

        #region IPhaseTaskメンバ

        public bool IsComplete { get; set; }
        public bool IsReflectMouseMove { get { return false; } }
        public void StartTask()
        {
            MainWindow.NewGamePanel.IsActive = true;
            MainWindow.NewGamePanel.PanelVisible = System.Windows.Visibility.Visible;
        }
        public void DoTimerTick()
        {
            if (MainWindow.NewGamePanel.IsActive == false)
            {
                IsComplete = true;
            }
            if (MainWindow.NewGamePanel.AutoNextGame && _isPauseAutoLoop == false)
            {
                IsComplete = true;
            }
        }
        public IPhaseTask EndTask()
        {
            MainWindow.NewGamePanel.PanelVisible = System.Windows.Visibility.Hidden;
            Game.Instance.PlaceRecord.ClearRecord();
            MainWindow.Board.SetStartingCells();
            MainWindow.InfoPanel.CountNumColor();
            Game.Instance.PlayerManager.SetPlayers(
                new Player(MainWindow.NewGamePanel.Player1Name,
                    CellState.Black,
                    MainWindow.NewGamePanel.Player1Kind),
                new Player(MainWindow.NewGamePanel.Player2Name,
                    CellState.White,
                    MainWindow.NewGamePanel.Player2Kind));
            Game.Instance.PlayerManager.ChangePlayer(0);
            var nextPlayerKind = 
                Game.Instance.PlayerManager.GetCurrentPlayer().PlayerKind;
            if (nextPlayerKind == PlayerKind.User)
            {
                return new UserTurnPhase();
            }
            else
            {
                return new ComTurnPhase();
            }
        }

        #endregion //IPhaseTaskメンバ

    }
}
