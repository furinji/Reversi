using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace ReversiWpf
{
    public class BoardViewModel : INotifyPropertyChanged
    {
        public BoardViewModel(BoardView boardView)
        {
            BoardView = boardView;
            SetCells();
            SetStartingCells();
            BoardView.RootGrid.MouseMove += new MouseEventHandler(RootGrid_MouseMove);
        }

        void RootGrid_MouseMove(object sender, MouseEventArgs e)
        {
            OnMoveMouse(e.GetPosition(BoardView.canvas1));
        }

        public BoardView BoardView { get; set; }

        private CellViewModel[,] _cellTable = new CellViewModel[8, 8];

        private void SetCells()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    var cell = CellViewModel.CreateInstance();
                    cell.PosIdx = new System.Drawing.Point(x, y);
                    _cellTable[y, x] = cell;
                    BoardView.canvas1.Children.Add(cell.CellView);
                }
            }
        }

        public CellViewModel GetCell(System.Drawing.Point posIdx)
        {
            return _cellTable[posIdx.Y, posIdx.X];
        }

        public void SetStartingCells()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    _cellTable[y, x].State = CellState.None;
                }
            }
            _cellTable[3, 3].State = CellState.White;
            _cellTable[3, 4].State = CellState.Black;
            _cellTable[4, 3].State = CellState.Black;
            _cellTable[4, 4].State = CellState.White;
        }

        #region INotifyPropertyChanged メンバ

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion //INotifyPropertyChanged メンバ


        public OverturnableInfo GetTurnableInfo(int idxX, int idxY)
        {
            if (_cellTable[idxY, idxX].State != CellState.None) { return null; }
            var result = new OverturnableInfo();
            result.IdxX = idxX;
            result.IdxY = idxY;
            var range = new Rect(0, 0, 7, 7);
            var myColor = Game.Instance.CurrentStone;
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var targetX = idxX;
                var targetY = idxY;
                int cntTurnable = 0;
                while (true)
                {
                    targetX += (int)_increaseTargetPos[direction].X;
                    targetY += (int)_increaseTargetPos[direction].Y;
                    if ((range.Contains(targetX, targetY) == false) ||
                        (_cellTable[targetY, targetX].State == CellState.None))
                    {
                        cntTurnable = 0;
                        break;
                    }
                    if (_cellTable[targetY, targetX].State == myColor)
                    {
                        break;
                    }
                    cntTurnable += 1;
                }
                result.NumberOfTurnable[direction] = cntTurnable;
                targetX = idxX + (int)_increaseTargetPos[direction].X;
                targetY = idxY + (int)_increaseTargetPos[direction].Y;
                if (range.Contains(targetX, targetY) != false)
                {
                    result.ExistStone[direction] =
                        (_cellTable[idxY, idxX].State != CellState.None);
                }
                else
                {
                    result.ExistStone[direction] = false;
                }
            }
            return result;
        }

        public void OverturnStones(OverturnableInfo info)
        {
            var myColor = _cellTable[info.IdxY, info.IdxX].State;
            foreach (var direction in info.NumberOfTurnable.Keys)
            {
                var targetX = info.IdxX;
                var targetY = info.IdxY;
                for (int i = 0; i < info.NumberOfTurnable[direction]; i++)
                {
                    targetX += (int)_increaseTargetPos[direction].X;
                    targetY += (int)_increaseTargetPos[direction].Y;
                    _cellTable[targetY, targetX].State = myColor;
                }
            }
        }

        public Tuple<int, int> GetNumberOfColor()
        {
            int blackNum = 0;
            int whiteNum = 0;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (_cellTable[y, x].State == CellState.Black)
                    {
                        blackNum += 1;
                    }
                    else if (_cellTable[y, x].State == CellState.White)
                    {
                        whiteNum += 1;
                    }
                }
            }
            return new Tuple<int, int>(blackNum, whiteNum);
        }

        public OverturnableInfo[] GetTurnableInfos()
        {
            var resultList = new List<OverturnableInfo>();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    var info = GetTurnableInfo(x, y);
                    if (info == null || info.CanPlace() == false) { continue; }
                    resultList.Add(info);
                }
            }
            return resultList.ToArray();
        }

        public bool CanPlace()
        {
            var turnables = GetTurnableInfos();
            if (turnables.Length < 1) { return false; }
            return true;
        }

        private void OnMoveMouse(Point pos)
        {
            int posX = -1;
            int posY = -1;
            var range = new Rect(0, 0, 319, 319);
            if (Game.Instance.PhaseManager.CurrentPhaseTask.IsReflectMouseMove)
            {
                if (range.Contains(new Point(pos.X, pos.Y)))
                {
                    posX = (int)pos.X / 40;
                    posY = (int)pos.Y / 40;
                }
            }
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (x == posX && y == posY)
                    {
                        var turnableInfo = GetTurnableInfo(x, y);
                        if (turnableInfo != null && turnableInfo.CanPlace())
                        {
                            if (Game.Instance.CurrentStone == CellState.Black)
                            {
                                _cellTable[y, x].EffectByMouseOver = EffectByMouseOver.MouseOverAndTemporaryBlack;
                            }
                            else
                            {
                                _cellTable[y, x].EffectByMouseOver = EffectByMouseOver.MouseOverAndTemporaryWhite;
                            }
                        }
                        else
                        {
                            _cellTable[y, x].EffectByMouseOver = EffectByMouseOver.MouseOver;
                        }
                    }
                    else
                    {
                        _cellTable[y, x].EffectByMouseOver = EffectByMouseOver.None;
                    }
                }
            }
        }

        private static Dictionary<Direction, Point> _increaseTargetPos =
            new Dictionary<Direction, Point>()
        {
            {Direction.Up, new Point(0, -1)},
            {Direction.UpRight, new Point(1, -1)},
            {Direction.Right, new Point(1, 0)},
            {Direction.DownRight, new Point(1, 1)},
            {Direction.Down, new Point(0, 1)},
            {Direction.DownLeft, new Point(-1, 1)},
            {Direction.Left, new Point(-1, 0)},
            {Direction.UpLeft, new Point(-1, -1)}
        };

    }
}
