using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public class PlayerManager
    {
        public PlayerManager()
        {
            _currentIdx = 0;
            _players = new Player[2];
        }

        private int _currentIdx;
        private Player[] _players;

        public int CurrentIdx { get { return _currentIdx; } }

        public void SetPlayers(Player player1, Player player2)
        {
            _players[0] = player1;
            _players[1] = player2;
            MainWindow.InfoPanel.Player1Name = _players[0].Name;
            MainWindow.InfoPanel.Player2Name = _players[1].Name;
        }

        public void SetPlayers()
        {
            SetPlayers(
                new Player(MainWindow.NewGamePanel.Player1Name,
                    CellState.Black,
                    MainWindow.NewGamePanel.Player1Kind),
                new Player(MainWindow.NewGamePanel.Player2Name,
                    CellState.White,
                    MainWindow.NewGamePanel.Player2Kind));
        }

        public Player GetCurrentPlayer()
        {
            return _players[_currentIdx];
        }

        public void ChangePlayer()
        {
            _currentIdx = 1 - _currentIdx;
            OnChangedPlayer();
        }

        public void ChangePlayer(int idx)
        {
            _currentIdx = idx;
            OnChangedPlayer();
        }

        public Player GetPlayer1()
        {
            return _players[0];
        }

        public Player GetPlayer2()
        {
            return _players[1];
        }

        private void OnChangedPlayer()
        {
            MainWindow.InfoPanel.Message = String.Format(
                    "{0}の番です", GetCurrentPlayer().Name);
            if (_currentIdx == 0)
            {
                MainWindow.InfoPanel.SetPlayer1Active(true);
                MainWindow.InfoPanel.SetPlayer2Active(false);
            }
            else
            {
                MainWindow.InfoPanel.SetPlayer1Active(false);
                MainWindow.InfoPanel.SetPlayer2Active(true);
            }
        }

    }
}
