using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public class GameOverPhase : IPhaseTask
    {
        public GameOverPhase()
        {
            IsComplete = false;
        }

        private int _startTime;

        #region IPhaseTaskメンバ

        public bool IsComplete { get; set; }
        public bool IsReflectMouseMove { get { return false; } }
        public void StartTask()
        {
            _startTime = System.Environment.TickCount;
            var msg = "";
            var nums = MainWindow.Board.GetNumberOfColor();
            var winStone = CellState.None;
            if (nums.Item1 > nums.Item2)
            {
                winStone = Game.Instance.PlayerManager.GetPlayer1().StoneColor;
                msg += Game.Instance.PlayerManager.GetPlayer1().Name +
                    "の勝ちです";
                Game.Instance.PlayerManager.AddWinCount(0, WinOrLose.Win);
                Game.Instance.PlayerManager.AddWinCount(1, WinOrLose.Lose);
            }
            else if (nums.Item1 < nums.Item2)
            {
                winStone = Game.Instance.PlayerManager.GetPlayer2().StoneColor;
                msg += Game.Instance.PlayerManager.GetPlayer2().Name +
                    "の勝ちです";
                Game.Instance.PlayerManager.AddWinCount(1, WinOrLose.Win);
                Game.Instance.PlayerManager.AddWinCount(0, WinOrLose.Lose);
            }
            else
            {
                msg += "引き分けです";
                Game.Instance.PlayerManager.AddWinCount(0, WinOrLose.Draw);
                Game.Instance.PlayerManager.AddWinCount(1, WinOrLose.Draw);
            }
            MainWindow.InfoPanel.Message = msg;
            MainWindow.InfoPanel.SetPlayerNameText();
        }
        public void DoTimerTick()
        {
            if (MainWindow.NewGamePanel.AutoNextGame)
            {
                if (System.Environment.TickCount - _startTime > 1000)
                {
                    IsComplete = true;
                }
            }
        }
        public IPhaseTask EndTask()
        {
            return new StartNewGamePhase();
        }

        #endregion //IPhaseTaskメンバ

    }
}
