using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public class OverturnableInfo
    {
        public OverturnableInfo()
        {
            NumberOfTurnable = new Dictionary<Direction, int>();
            ExistStone = new Dictionary<Direction, bool>();
        }

        public int IdxX { get; set; }
        public int IdxY { get; set; }
        public Dictionary<Direction, int> NumberOfTurnable { get; private set; }
        public Dictionary<Direction, bool> ExistStone { get; private set; }

        public bool CanPlace()
        {
            foreach (var item in NumberOfTurnable.Values)
            {
                if (item > 0) { return true; }
            }
            return false;
        }

        public int GetNumberOfTurnable()
        {
            int resultNum = 0;
            foreach (var num in NumberOfTurnable)
            {
                resultNum += num.Value;
            }
            return resultNum;
        }

        public System.Drawing.Point[] GetOverTurnPosArray()
        {
            var resultList = new List<System.Drawing.Point>();
            foreach (var direction in NumberOfTurnable.Keys)
            {
                var targetX = IdxX;
                var targetY = IdxY;
                for (int i = 0; i < NumberOfTurnable[direction]; i++)
                {
                    targetX += _increaseTargetPos[direction].X;
                    targetY += _increaseTargetPos[direction].Y;
                    resultList.Add(new System.Drawing.Point(targetX, targetY));
                }
            }
            return resultList.ToArray();
        }

        private static Dictionary<Direction, System.Drawing.Point> _increaseTargetPos =
            new Dictionary<Direction, System.Drawing.Point>()
        {
            {Direction.Up, new System.Drawing.Point(0, -1)},
            {Direction.UpRight, new System.Drawing.Point(1, -1)},
            {Direction.Right, new System.Drawing.Point(1, 0)},
            {Direction.DownRight, new System.Drawing.Point(1, 1)},
            {Direction.Down, new System.Drawing.Point(0, 1)},
            {Direction.DownLeft, new System.Drawing.Point(-1, 1)},
            {Direction.Left, new System.Drawing.Point(-1, 0)},
            {Direction.UpLeft, new System.Drawing.Point(-1, -1)}
        };

    }
}
