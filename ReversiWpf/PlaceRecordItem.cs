using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public class PlaceRecordItem
    {
        public PlaceRecordItem(
            OverturnableInfo placeAndTurnableInfo,
            CellState stoneColor)
        {
            PlaceAndTurnableInfo = placeAndTurnableInfo;
            StoneColor = stoneColor;
        }

        public OverturnableInfo PlaceAndTurnableInfo { get; set; }
        public CellState StoneColor { get; set; }
    }
}
