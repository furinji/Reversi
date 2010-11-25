using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public class PhaseManager
    {
        public PhaseManager()
        {
            _currentPhaseTask = new StartNewGamePhase();
            ClickInfo = null;
        }

        public ClickInfo ClickInfo { get; set; }
        public IPhaseTask CurrentPhaseTask
        {
            get { return _currentPhaseTask; }
            set { _currentPhaseTask = value; }
        }

        private IPhaseTask _currentPhaseTask;

        public void TimerTick(ClickInfo clickInfo)
        {
            ClickInfo = clickInfo;
            if (_currentPhaseTask == null) { return; }
            if (_currentPhaseTask.IsComplete)
            {
                var nextPhaseKind = _currentPhaseTask.EndTask();
                _currentPhaseTask = nextPhaseKind;
                _currentPhaseTask.StartTask();
            }
            else
            {
                _currentPhaseTask.DoTimerTick();
            }
        }

        public void SetTask(IPhaseTask task)
        {
            _currentPhaseTask = task;
            _currentPhaseTask.StartTask();
        }

    }
}
