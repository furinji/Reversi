using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public class WinRateSample
    {
        public WinRateSample()
        {

        }
        public WinRateSample(double winRate, uint numberOfSample)
        {
            WinRate = winRate;
            NumberOfSample = numberOfSample;
        }

        public double WinRate { get; set; }
        public uint NumberOfSample { get; set; }

        public void AddSample(bool isWin)
        {
            double numWin = WinRate * (double)NumberOfSample;
            double numLose = (double)NumberOfSample - numWin;
            if (isWin)
            {
                numWin += 1.0;
            }
            else
            {
                numLose += 1.0;
            }
            WinRate = numWin / (numWin + numLose);
            if (numWin + numLose < (double)uint.MaxValue)
            {
                NumberOfSample = (uint)(numWin + numLose);
            }
            else
            {
                NumberOfSample = uint.MaxValue;
            }
        }

    }
}
