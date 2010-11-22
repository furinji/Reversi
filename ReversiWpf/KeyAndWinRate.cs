using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public class KeyAndWinRate
    {
        public KeyAndWinRate()
        {
            Key = 0;
            WinRate = null;
        }
        public KeyAndWinRate(int key, WinRateSample winRate)
        {
            Key = key;
            WinRate = winRate;
        }

        public int Key { get; set; }
        public WinRateSample WinRate { get; set; }
    }
}
