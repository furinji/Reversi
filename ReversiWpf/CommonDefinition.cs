using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public enum CellState
    {
        None, Black, White
    }

    public enum Direction
    {
        Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft
    }

    public enum EffectByMouseOver
    {
        None, MouseOver, MouseOverAndTemporaryBlack, MouseOverAndTemporaryWhite
    }

    public enum PlayerKind
    {
        User, Com
    }

    public enum PhaseKind
    {
        OutGame, UserTurn, ComTurn, OverTurnStones, ChangePlayer
    }

    public class CommonDefinition
    {


    }
}
