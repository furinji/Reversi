using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace ReversiWpf
{
    public class Game
    {
        public Game ()
	    {
            _playerManager = new PlayerManager();
            _phaseManager = new PhaseManager();
            _placeRecord = new PlaceRecord();
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Start();
	    }

        void _timer_Tick(object sender, EventArgs e)
        {
            var clickInfo = GetAndSetClickInfo(null);
            _phaseManager.TimerTick(clickInfo);
        }

        

        private static Game _instance;
        public static Game Instance
        {
            get
            {
                if (_instance == null) { _instance = new Game(); }
                return _instance;
            }
        }

        public CellState CurrentStone
        {
            get { return _playerManager.GetCurrentPlayer().StoneColor; }
        }

        public PlayerManager PlayerManager { get { return _playerManager; } }
        public PhaseManager PhaseManager { get { return _phaseManager; } }
        public PlaceRecord PlaceRecord { get { return _placeRecord; } }
        public ClickInfo ClickInfo
        {
            get 
            {
                lock (_syncClickInfo)
                {
                    return _clickInfo;
                }
            }
            set 
            {
                lock (_syncClickInfo)
                {
                    _clickInfo = value;
                }
            }
        }
        public ClickInfo GetAndSetClickInfo(ClickInfo info)
        {
            ClickInfo resultInfo;
            lock (_syncClickInfo)
            {
                resultInfo = _clickInfo;
                _clickInfo = info;
            }
            return resultInfo;
        }

        private DispatcherTimer _timer;
        private PlayerManager _playerManager;
        private ClickInfo _clickInfo;
        private object _syncClickInfo = new object();
        private PhaseManager _phaseManager;
        private PlaceRecord _placeRecord;

        public void NewGame(string param)
        {
            _phaseManager.SetTask(new StartNewGamePhase(param));
            MainWindow.Board.SetStartingCells();
            MainWindow.InfoPanel.CountNumColor();
            SetPlayers();
            _playerManager.ChangePlayer(0);
        }

        public void OnClicked(System.Drawing.Point posIdx)
        {
            ClickInfo = new ClickInfo(posIdx);
        }


        private void SetPlayers()
        {
            _playerManager.SetPlayers();
        }

    }
}
