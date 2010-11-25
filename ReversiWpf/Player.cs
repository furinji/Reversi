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

    }
}
