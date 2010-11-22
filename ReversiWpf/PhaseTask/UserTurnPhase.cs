using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public class UserTurnPhase : IPhaseTask
    {
        public UserTurnPhase()
        {
            IsComplete = false;
            _overTurnInfo = null;
        }

        private OverturnableInfo _overTurnInfo;
        private int _startMilsec;

        #region IPhaseTaskメンバ

        public bool IsComplete { get; set; }
        public bool IsReflectMouseMove { get { return true; } }
        public void StartTask()
        {
            _startMilsec = System.Environment.TickCount;
            if (MainWindow.Board.CanPlace() == false)
            {
                IsComplete = true;
            }
        }
        public void DoTimerTick()
        {
            int sec = (System.Environment.TickCount - _startMilsec) / 1000;
            MainWindow.InfoPanel.SetPlayerMessage(
                String.Format("考え中 {0}秒", sec),
                Game.Instance.PlayerManager.CurrentIdx);
            if (Game.Instance.PhaseManager.ClickInfo == null) { return; }
            var clickPosIdx = Game.Instance.PhaseManager.ClickInfo.Position;
            _overTurnInfo = MainWindow.Board.GetTurnableInfo(
                clickPosIdx.X, clickPosIdx.Y);
            if (_overTurnInfo == null || _overTurnInfo.GetNumberOfTurnable() <= 0) 
            { 
                return;
            }
            IsComplete = true;
        }
        public IPhaseTask EndTask()
        {
            var stoneColor =
                Game.Instance.PlayerManager.GetCurrentPlayer().StoneColor;
            Game.Instance.PlaceRecord.AddRecord(
                new PlaceRecordItem(_overTurnInfo, stoneColor));
            //WritePlayerMessage(_overTurnInfo);
            MainWindow.InfoPanel.SetPlayerMessage(_overTurnInfo,
                Game.Instance.PlayerManager.CurrentIdx);
            if (_overTurnInfo == null)
            {
                return new OverTurnStonesPhase(stoneColor);
            }
            var placePos = new System.Drawing.Point(
                _overTurnInfo.IdxX, _overTurnInfo.IdxY);
            var turnPosArray = _overTurnInfo.GetOverTurnPosArray();
            return new OverTurnStonesPhase(placePos, turnPosArray, stoneColor);
        }

        #endregion

    }
}
