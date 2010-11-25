using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public class WinLoseCount
    {
        public WinLoseCount()
        {
            Win = 0;
            Lose = 0;
            Draw = 0;
        }

        public int Win { get; set; }
        public int Lose { get; set; }
        public int Draw { get; set; }

        public void AddCount(WinOrLose winOrLose)
        {
            switch (winOrLose)
            {
                case WinOrLose.Win:
                    Win += 1;
                    break;
                case WinOrLose.Lose:
                    Lose += 1;
                    break;
                case WinOrLose.Draw:
                    Draw += 1;
                    break;
                default:
                    break;
            }
        }

        public string ToText()
        {
            var result = "";
            if (Win > 0) { result += Win.ToString() + "勝"; }
            if (Lose > 0) { result += Lose.ToString() + "敗"; }
            if (Draw > 0) { result += Draw.ToString() + "分"; }
            return result;
        }
    }
}
