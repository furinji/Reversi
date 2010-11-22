using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public interface IPhaseTask
    {
        bool IsComplete { get; set; }
        bool IsReflectMouseMove { get; }
        void StartTask();
        void DoTimerTick();
        IPhaseTask EndTask();
    }
}
