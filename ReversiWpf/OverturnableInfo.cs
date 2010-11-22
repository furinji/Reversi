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
        }

        public int IdxX { get; set; }
        public int IdxY { get; set; }
        public Dictionary<Direction, int> NumberOfTurnable { get; private set; }

        public bool CanPlace()
        {
            foreach (var item in NumberOfTurnable.Values)
            {
                if (item > 0) { return true; }
            }
            return false;
        }

        public int CreateKey()
        {
            int key = 0;
            key += IdxX;
            key += IdxY * 8;
            foreach (Direction direction in NumberOfTurnable.Keys)
            {
                switch (direction)
                {
                    case Direction.Up:
                        key += NumberOfTurnable[direction] * (int)Math.Pow(8, 2);
                        break;
                    case Direction.UpRight:
                        key += NumberOfTurnable[direction] * (int)Math.Pow(8, 3);
                        break;
                    case Direction.Right:
                        key += NumberOfTurnable[direction] * (int)Math.Pow(8, 4);
                        break;
                    case Direction.DownRight:
                        key += NumberOfTurnable[direction] * (int)Math.Pow(8, 5);
                        break;
                    case Direction.Down:
                        key += NumberOfTurnable[direction] * (int)Math.Pow(8, 6);
                        break;
                    case Direction.DownLeft:
                        key += NumberOfTurnable[direction] * (int)Math.Pow(8, 7);
                        break;
                    case Direction.Left:
                        key += NumberOfTurnable[direction] * (int)Math.Pow(8, 8);
                        break;
                    case Direction.UpLeft:
                        key += NumberOfTurnable[direction] * (int)Math.Pow(8, 9);
                        break;
                }
            }
            return key;
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

        public System.Drawing.Point[] GetOverTurnPosArray()
        {
            //var myColor = _cellTable[info.IdxY, info.IdxX].State;
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

        public OverturnableInfo ConvertToBase()
        {
            var resultInfo = new OverturnableInfo();
            if (IdxX < 4 && IdxY < 4 && IdxY <= IdxX)
            {

            }
            else if (IdxX < 4 && IdxY < 4)
            {
                resultInfo.IdxX = IdxY;
                resultInfo.IdxY = IdxX;
                resultInfo.NumberOfTurnable[Direction.Up] = NumberOfTurnable[Direction.Left];
                resultInfo.NumberOfTurnable[Direction.UpRight] = NumberOfTurnable[Direction.DownLeft];
                resultInfo.NumberOfTurnable[Direction.Right] = NumberOfTurnable[Direction.Down];
                resultInfo.NumberOfTurnable[Direction.DownRight] = NumberOfTurnable[Direction.DownRight];
                resultInfo.NumberOfTurnable[Direction.Down] = NumberOfTurnable[Direction.Right];
                resultInfo.NumberOfTurnable[Direction.DownLeft] = NumberOfTurnable[Direction.UpRight];
                resultInfo.NumberOfTurnable[Direction.Left] = NumberOfTurnable[Direction.Up];
                resultInfo.NumberOfTurnable[Direction.UpLeft] = NumberOfTurnable[Direction.UpLeft];
            }
            else if (IdxX > 3 && IdxY < 4 && IdxY <= 7 - IdxX)
            {
                resultInfo.IdxX = 7 - IdxX;
                resultInfo.IdxY = IdxY;
                resultInfo.NumberOfTurnable[Direction.Up] = NumberOfTurnable[Direction.Up];
                resultInfo.NumberOfTurnable[Direction.UpRight] = NumberOfTurnable[Direction.UpLeft];
                resultInfo.NumberOfTurnable[Direction.Right] = NumberOfTurnable[Direction.Left];
                resultInfo.NumberOfTurnable[Direction.DownRight] = NumberOfTurnable[Direction.DownLeft];
                resultInfo.NumberOfTurnable[Direction.Down] = NumberOfTurnable[Direction.Down];
                resultInfo.NumberOfTurnable[Direction.DownLeft] = NumberOfTurnable[Direction.DownRight];
                resultInfo.NumberOfTurnable[Direction.Left] = NumberOfTurnable[Direction.Right];
                resultInfo.NumberOfTurnable[Direction.UpLeft] = NumberOfTurnable[Direction.UpRight];
            }
            else if (IdxX > 3 && IdxY < 4)
            {
                resultInfo.IdxX = IdxY;
                resultInfo.IdxY = 7 - IdxX;
                resultInfo.NumberOfTurnable[Direction.Up] = NumberOfTurnable[Direction.Right];
                resultInfo.NumberOfTurnable[Direction.UpRight] = NumberOfTurnable[Direction.DownRight];
                resultInfo.NumberOfTurnable[Direction.Right] = NumberOfTurnable[Direction.Down];
                resultInfo.NumberOfTurnable[Direction.DownRight] = NumberOfTurnable[Direction.DownLeft];
                resultInfo.NumberOfTurnable[Direction.Down] = NumberOfTurnable[Direction.Left];
                resultInfo.NumberOfTurnable[Direction.DownLeft] = NumberOfTurnable[Direction.UpLeft];
                resultInfo.NumberOfTurnable[Direction.Left] = NumberOfTurnable[Direction.Up];
                resultInfo.NumberOfTurnable[Direction.UpLeft] = NumberOfTurnable[Direction.UpRight];
            }
            else if (IdxX < 4 && 7 - IdxY <= IdxX)
            {
                resultInfo.IdxX = IdxX;
                resultInfo.IdxY = 7 - IdxY;
                resultInfo.NumberOfTurnable[Direction.Up] = NumberOfTurnable[Direction.Down];
                resultInfo.NumberOfTurnable[Direction.UpRight] = NumberOfTurnable[Direction.DownRight];
                resultInfo.NumberOfTurnable[Direction.Right] = NumberOfTurnable[Direction.Right];
                resultInfo.NumberOfTurnable[Direction.DownRight] = NumberOfTurnable[Direction.UpRight];
                resultInfo.NumberOfTurnable[Direction.Down] = NumberOfTurnable[Direction.Up];
                resultInfo.NumberOfTurnable[Direction.DownLeft] = NumberOfTurnable[Direction.UpLeft];
                resultInfo.NumberOfTurnable[Direction.Left] = NumberOfTurnable[Direction.Left];
                resultInfo.NumberOfTurnable[Direction.UpLeft] = NumberOfTurnable[Direction.DownLeft];
            }
            else if (IdxX < 4)
            {
                resultInfo.IdxX = 7 - IdxY;
                resultInfo.IdxY = IdxX;
                resultInfo.NumberOfTurnable[Direction.Up] = NumberOfTurnable[Direction.Left];
                resultInfo.NumberOfTurnable[Direction.UpRight] = NumberOfTurnable[Direction.UpLeft];
                resultInfo.NumberOfTurnable[Direction.Right] = NumberOfTurnable[Direction.Up];
                resultInfo.NumberOfTurnable[Direction.DownRight] = NumberOfTurnable[Direction.UpRight];
                resultInfo.NumberOfTurnable[Direction.Down] = NumberOfTurnable[Direction.Right];
                resultInfo.NumberOfTurnable[Direction.DownLeft] = NumberOfTurnable[Direction.DownRight];
                resultInfo.NumberOfTurnable[Direction.Left] = NumberOfTurnable[Direction.Down];
                resultInfo.NumberOfTurnable[Direction.UpLeft] = NumberOfTurnable[Direction.DownLeft];
            }
            else if (IdxX <= IdxY)
            {
                resultInfo.IdxX = 7 - IdxX;
                resultInfo.IdxY = 7 - IdxY;
                resultInfo.NumberOfTurnable[Direction.Up] = NumberOfTurnable[Direction.Down];
                resultInfo.NumberOfTurnable[Direction.UpRight] = NumberOfTurnable[Direction.DownLeft];
                resultInfo.NumberOfTurnable[Direction.Right] = NumberOfTurnable[Direction.Left];
                resultInfo.NumberOfTurnable[Direction.DownRight] = NumberOfTurnable[Direction.UpLeft];
                resultInfo.NumberOfTurnable[Direction.Down] = NumberOfTurnable[Direction.Up];
                resultInfo.NumberOfTurnable[Direction.DownLeft] = NumberOfTurnable[Direction.UpRight];
                resultInfo.NumberOfTurnable[Direction.Left] = NumberOfTurnable[Direction.Right];
                resultInfo.NumberOfTurnable[Direction.UpLeft] = NumberOfTurnable[Direction.DownRight];
            }
            else
            {
                resultInfo.IdxX = 7 - IdxY;
                resultInfo.IdxY = 7 - IdxX;
                resultInfo.NumberOfTurnable[Direction.Up] = NumberOfTurnable[Direction.Right];
                resultInfo.NumberOfTurnable[Direction.UpRight] = NumberOfTurnable[Direction.UpRight];
                resultInfo.NumberOfTurnable[Direction.Right] = NumberOfTurnable[Direction.Up];
                resultInfo.NumberOfTurnable[Direction.DownRight] = NumberOfTurnable[Direction.UpLeft];
                resultInfo.NumberOfTurnable[Direction.Down] = NumberOfTurnable[Direction.Left];
                resultInfo.NumberOfTurnable[Direction.DownLeft] = NumberOfTurnable[Direction.DownLeft];
                resultInfo.NumberOfTurnable[Direction.Left] = NumberOfTurnable[Direction.Down];
                resultInfo.NumberOfTurnable[Direction.UpLeft] = NumberOfTurnable[Direction.DownRight];
            }
            return resultInfo;
        }

    }
}
