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
            }
            else if (nums.Item1 < nums.Item2)
            {
                winStone = Game.Instance.PlayerManager.GetPlayer2().StoneColor;
                msg += Game.Instance.PlayerManager.GetPlayer2().Name +
                    "の勝ちです";
            }
            else
            {
                msg += "引き分けです";
            }
            MainWindow.InfoPanel.Message = msg;
            if (winStone != CellState.None)
            {
                KeyAndWinRateCollection.Instance.SetWinRates(
                    Game.Instance.PlaceRecord.GetItems(), winStone);
                KeyAndWinRateCollection.Instance.SaveToFile();
            }

        }
        public void DoTimerTick()
        {
            //if (System.Environment.TickCount - _startTime > 1000)
            //{
            //    Game.Instance.IsAutoLoop = true;
            //    IsComplete = true;
            //}
        }
        public IPhaseTask EndTask()
        {
            return new StartNewGamePhase();
        }

        #endregion

    }
}
