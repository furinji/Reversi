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
            WinCountDic = new Dictionary<string, WinLoseCount>();
        }


        private int _currentIdx;
        private Player[] _players;


        public int CurrentIdx { get { return _currentIdx; } }
        public Dictionary<string, WinLoseCount> WinCountDic { get; set; }


        public void SetPlayers(Player player1, Player player2)
        {
            _players[0] = player1;
            _players[1] = player2;
            MainWindow.InfoPanel.SetPlayerNameText();
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

        public string GetTextPlayerName(int playerIdx)
        {
            var name = _players[playerIdx].Name;
            var winText = (WinCountDic.ContainsKey(name)) ?
                WinCountDic[name].ToText() : "";
            return String.Format("{0} {1}", name, winText);
        }

        public void AddWinCount(int playerIdx, WinOrLose winOrLose)
        {
            var name = _players[playerIdx].Name;
            if (WinCountDic.ContainsKey(name) == false)
            {
                WinCountDic[name] = new WinLoseCount();
            }
            WinCountDic[name].AddCount(winOrLose);
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
