using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public class OverTurnStonesPhase : IPhaseTask
    {
        public OverTurnStonesPhase(System.Drawing.Point placePos,
            System.Drawing.Point[] turnPosArray,
            CellState colorToTurn)
        {
            _placePos = placePos;
            _turnPosList = turnPosArray.ToList();
            _colorToTurn = colorToTurn;
            _turnPosList.Add(_placePos);
            Init();
        }
        public OverTurnStonesPhase(CellState colorToTurn)
        {
            _placePos = new System.Drawing.Point(-1, -1);
            _turnPosList = new List<System.Drawing.Point>();
            _colorToTurn = colorToTurn;
            Init();
        }
        private void Init()
        {
            IsComplete = false;
            _startTime = System.Environment.TickCount;
            _waitMilsec = 200;
        }

        private System.Drawing.Point _placePos;
        private List<System.Drawing.Point> _turnPosList;
        private CellState _colorToTurn;
        private int _startTime;
        private int _waitMilsec;

        #region IPhaseTaskメンバ

        public bool IsComplete { get; set; }
        public bool IsReflectMouseMove { get { return false; } }
        public void StartTask()
        {

        }
        public void DoTimerTick()
        {
            var cntDo = (System.Environment.TickCount - _startTime) / _waitMilsec;
            for (int i = _turnPosList.Count - 1; i >= 0; i--)
            {
                var dif = Math.Max(
                    (int)Math.Abs(_turnPosList[i].X - _placePos.X),
                    (int)Math.Abs(_turnPosList[i].Y - _placePos.Y));
                if (dif <= cntDo)
                {
                    MainWindow.Board.GetCell(_turnPosList[i]).State = _colorToTurn;
                    _turnPosList.RemoveAt(i);
                }
            }
            if (_turnPosList.Count <= 0) { IsComplete = true; }
        }
        public IPhaseTask EndTask()
        {
            MainWindow.InfoPanel.CountNumColor();
            return new ChangePlayerPhase();
        }

        #endregion //IPhaseTaskメンバ

    }
}
