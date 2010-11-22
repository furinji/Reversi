using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public class Player
    {
        public Player(string name, CellState stoneColor, PlayerKind playerKind)
        {
            Name = name;
            StoneColor = stoneColor;
            this.PlayerKind = playerKind;
        }

        public string Name { get; set; }
        public CellState StoneColor { get; private set; }
        public PlayerKind PlayerKind { get; set; }

        public System.Drawing.Point? ChoicePlacePos()
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

        private int GetValueRank(OverturnableInfo info)
        {
            var specialRanks = new Dictionary<System.Drawing.Point, int>()
            {
                {new System.Drawing.Point(0, 0), 10},
                {new System.Drawing.Point(7, 0), 10},
                {new System.Drawing.Point(0, 7), 10},
                {new System.Drawing.Point(7, 7), 10},
                {new System.Drawing.Point(2, 0), 9},
                {new System.Drawing.Point(2, 1), 9},
                {new System.Drawing.Point(0, 2), 9},
                {new System.Drawing.Point(1, 2), 9},
                {new System.Drawing.Point(2, 2), 9},
                {new System.Drawing.Point(5, 0), 9},
                {new System.Drawing.Point(5, 1), 9},
                {new System.Drawing.Point(5, 2), 9},
                {new System.Drawing.Point(6, 2), 9},
                {new System.Drawing.Point(7, 2), 9},
                {new System.Drawing.Point(0, 5), 9},
                {new System.Drawing.Point(1, 5), 9},
                {new System.Drawing.Point(2, 5), 9},
                {new System.Drawing.Point(2, 6), 9},
                {new System.Drawing.Point(2, 7), 9},
                {new System.Drawing.Point(5, 5), 9},
                {new System.Drawing.Point(6, 5), 9},
                {new System.Drawing.Point(7, 5), 9},
                {new System.Drawing.Point(5, 6), 9},
                {new System.Drawing.Point(5, 7), 9},
                {new System.Drawing.Point(3, 0), 8},
                {new System.Drawing.Point(4, 0), 8},
                {new System.Drawing.Point(0, 3), 8},
                {new System.Drawing.Point(0, 4), 8},
                {new System.Drawing.Point(7, 3), 8},
                {new System.Drawing.Point(7, 4), 8},
                {new System.Drawing.Point(3, 7), 8},
                {new System.Drawing.Point(4, 7), 8},
                {new System.Drawing.Point(1, 0), 0},
                {new System.Drawing.Point(0, 1), 0},
                {new System.Drawing.Point(1, 1), 0},
                {new System.Drawing.Point(6, 0), 0},
                {new System.Drawing.Point(6, 1), 0},
                {new System.Drawing.Point(7, 1), 0},
                {new System.Drawing.Point(0, 6), 0},
                {new System.Drawing.Point(1, 6), 0},
                {new System.Drawing.Point(1, 7), 0},
                {new System.Drawing.Point(6, 6), 0},
                {new System.Drawing.Point(7, 6), 0},
                {new System.Drawing.Point(6, 7), 0}
            };
            if (specialRanks.ContainsKey(
                new System.Drawing.Point(info.IdxX, info.IdxY)))
            {
                return specialRanks[new System.Drawing.Point(info.IdxX, info.IdxY)];
            }
            else
            {
                return info.GetNumberOfTurnable();
            }
        }

    }
}
