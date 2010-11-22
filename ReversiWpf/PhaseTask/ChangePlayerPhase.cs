using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public class ChangePlayerPhase : IPhaseTask
    {
        public ChangePlayerPhase()
        {
            IsComplete = false;
            _isGameOver = false;
        }

        private bool _isGameOver;

        #region IPhaseTaskメンバ

        public bool IsComplete { get; set; }
        public bool IsReflectMouseMove { get { return false; } }
        public void StartTask()
        {
            if (Game.Instance.PlaceRecord.GetLastPassCount() >= 2)
            {
                _isGameOver = true;
            }
            else
            {
                Game.Instance.PlayerManager.ChangePlayer();
                MainWindow.InfoPanel.SetPlayerMessage("考え中", 
                    Game.Instance.PlayerManager.CurrentIdx);
            }
            IsComplete = true;
        }
        public void DoTimerTick()
        {
            
        }
        public IPhaseTask EndTask()
        {
            if (_isGameOver)
            {
                return new GameOverPhase();
            }
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

        #endregion

    }
}
