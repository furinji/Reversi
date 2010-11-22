using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public class ComTurnPhase : IPhaseTask
    {
        public ComTurnPhase()
        {
            IsComplete = false;
            _overTurnInfo = null;
            _waitMinimum = _waitValuePlace;
        }

        private OverturnableInfo _overTurnInfo;
        private int _startMilsec;
        private int _waitMinimum;
        private readonly int _waitValuePlace = 200;
        private readonly int _waitValuePass = 200;
        private int[,] _valueTable = new int[,]{
                {120, -20,  20,   5,   5,  20, -20, 120},
                {-20, -40,  -5,  -5,  -5,  -5, -40, -20},
                { 20,  -5,  15,   3,   3,  15,  -5,  20},
                {  5,  -5,   3,   3,   3,   3,  -5,   5},
                {  5,  -5,   3,   3,   3,   3,  -5,   5},
                { 20,  -5,  15,   3,   3,  15,  -5,  20},
                {-20, -40,  -5,  -5,  -5,  -5, -40, -20},
                {120, -20,  20,   5,   5,  20, -20, 120}
            };

        #region IPhaseTaskメンバ

        public bool IsComplete { get; set; }
        public bool IsReflectMouseMove { get { return false; } }
        public void StartTask()
        {
            _startMilsec = System.Environment.TickCount;
            System.Drawing.Point? placePos;
            if (Game.Instance.PlayerManager.CurrentIdx == 0)
            {
                placePos = ChoicePlacePos();
            }
            else
            {
                placePos = ChoicePlacePos2();
            }
            if (placePos == null)
            {
                _waitMinimum = _waitValuePass;
                _overTurnInfo = null;
            }
            else
            {
                _waitMinimum = _waitValuePlace;
                var pos = (System.Drawing.Point)placePos;
                _overTurnInfo = MainWindow.Board.GetTurnableInfo(pos.X, pos.Y);
            }
        }
        public void DoTimerTick()
        {
            int sec = (System.Environment.TickCount - _startMilsec) / 1000;
            MainWindow.InfoPanel.SetPlayerMessage(
                String.Format("考え中 {0}秒", sec),
                Game.Instance.PlayerManager.CurrentIdx);
            if (System.Environment.TickCount - _startMilsec > _waitMinimum)
            {
                IsComplete = true;
            }
        }
        public IPhaseTask EndTask()
        {
            var stoneColor =
                Game.Instance.PlayerManager.GetCurrentPlayer().StoneColor;
            Game.Instance.PlaceRecord.AddRecord(
                new PlaceRecordItem(_overTurnInfo, stoneColor));
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

        private System.Drawing.Point? ChoicePlacePos()
        {
            var canPlaceCells = MainWindow.Board.GetTurnableInfos();
            OverturnableInfo resultCell = null;
            int maxTurnable = -1;
            foreach (var item in canPlaceCells)
            {
                if (maxTurnable < 0 || item.GetNumberOfTurnable() > maxTurnable)
                {
                    maxTurnable = GetValueRank(item);
                    resultCell = item;
                }
            }
            if (resultCell == null) { return null; }
            return new System.Drawing.Point(resultCell.IdxX, resultCell.IdxY);
        }

        private System.Drawing.Point? ChoicePlacePos2()
        {
            var canPlaceCells = MainWindow.Board.GetTurnableInfos();
            OverturnableInfo resultCell = null;
            foreach (var item in canPlaceCells)
            {
                double newValue = KeyAndWinRateCollection.Instance.GetWinRate(
                    item.ConvertToBase().CreateKey());
                double oldValue = 0.0;
                if (resultCell != null)
                {
                    oldValue = KeyAndWinRateCollection.Instance.GetWinRate(
                        resultCell.ConvertToBase().CreateKey());
                }
                if (resultCell == null || newValue > oldValue)
                {
                    resultCell = item;
                }
            }
            if (resultCell == null) { return null; }
            return new System.Drawing.Point(resultCell.IdxX, resultCell.IdxY);
        }

        private int GetValueRank(OverturnableInfo info)
        {
            var resultValue = 
                _valueTable[info.IdxY, info.IdxX] + info.GetNumberOfTurnable();
            return resultValue;
        }

    }
}