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
            Init();
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

        public void Init()
        {
            //CurrentStone = CellState.Black;
        }

        public bool IsAutoLoop = false;

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

        private void SetPlayers()
        {
            _playerManager.SetPlayers();
        }

        //public void ChangePlayer()
        //{
        //    System.Diagnostics.Debug.WriteLine(String.Format(
        //        "Change前 : {0}", _playerManager.GetCurrentPlayer().Name));
        //    _playerManager.ChangePlayer();
        //    System.Diagnostics.Debug.WriteLine(String.Format(
        //        "Change後 : {0}", _playerManager.GetCurrentPlayer().Name));
        //    if (_playerManager.GetCurrentPlayer().PlayerKind == PlayerKind.User)
        //    {
        //        ActivePhase = PhaseKind.UserTurn;
        //    }
        //    else
        //    {
        //        ActivePhase = PhaseKind.ComTurn;
        //    }
        //    if (_playerManager.CurrentIdx == 0)
        //    {
        //        MainWindow.IfPanel.Player1Message = "考え中";
        //    }
        //    else
        //    {
        //        MainWindow.IfPanel.Player2Message = "考え中";
        //    }
        //    var msg = _cntPass > 0 ? "パス  " : "";
        //    if (_playerManager.GetCurrentPlayer().StoneColor == CellState.Black)
        //    {
        //        MainWindow.IfPanel.Message = msg + "次は黒の番です";
        //    }
        //    else
        //    {
        //        MainWindow.IfPanel.Message = msg + "次は白の番です";
        //    }
        //    if (MainWindow.Board.CanPlace())
        //    {
        //        _cntPass = 0;
        //    }
        //    else
        //    {
        //        if (_playerManager.CurrentIdx == 0)
        //        {
        //            MainWindow.IfPanel.Player1Message = "パス";
        //        }
        //        else
        //        {
        //            MainWindow.IfPanel.Player2Message = "パス";
        //        }
        //        _cntPass += 1;
        //        if (_cntPass > 1)
        //        {
        //            var stoneNums = MainWindow.Board.GetNumberOfColor();
        //            var winMsg = "";
        //            if (stoneNums.Item1 > stoneNums.Item2)
        //            {
        //                winMsg = "  黒の勝ち";
        //            }
        //            else if (stoneNums.Item1 < stoneNums.Item2)
        //            {
        //                winMsg = "  白の勝ち";
        //            }
        //            else
        //            {
        //                winMsg = "  引き分け";
        //            }
        //            MainWindow.IfPanel.Message = "ゲーム終了" + winMsg;
        //            //IsGameMode = false;
        //            ActivePhase = PhaseKind.OutGame;
        //        }
        //        else
        //        {
        //            ChangePlayer();
        //        }
        //    }
        //}

        public void NewGame()
        {
            _phaseManager.SetTask(new StartNewGamePhase());
            MainWindow.Board.SetStartingCells();
            MainWindow.InfoPanel.CountNumColor();
            SetPlayers();
            _playerManager.ChangePlayer(0);
        }

        public void OnClicked(System.Drawing.Point posIdx)
        {
            ClickInfo = new ClickInfo(posIdx);
        }

        //private void PlaceStone(System.Drawing.Point posIdx)
        //{
        //    System.Diagnostics.Debug.WriteLine(String.Format("PlaceStone : {0}",
        //        _playerManager.GetCurrentPlayer().Name));
        //    var info = MainWindow.Board.GetTurnableInfo(posIdx.X, posIdx.Y);
        //    MainWindow.Board.GetCell(posIdx).State = _playerManager.GetCurrentPlayer().StoneColor;
        //    //MainWindow.Board.OverturnStones(info);
        //    lock (_syncLoopTask)
        //    {
        //        _loopTask = new OverTurnStonesTask(posIdx,
        //            info.GetOverTurnPosArray(),
        //            CurrentStone,
        //            () => { AfterPlaceStone(posIdx); });
        //    }
        //    System.Diagnostics.Debug.WriteLine(String.Format("looptask.IsComplete : {0}",
        //        _loopTask.IsComplete));
        //    //_loopTask.ActionAfterComplete = () => { AfterPlaceStone(posIdx); };
        //    //_loopTask.ActionAfterComplete = Action() { AfterPlaceStone(posIdx) };
        //}

        //private void AfterPlaceStone(System.Drawing.Point posIdx)
        //{
        //    MainWindow.Board.CountNumColor();
        //    if (_playerManager.CurrentIdx == 0)
        //    {
        //        MainWindow.IfPanel.Player1Message = posIdx.ToString();
        //    }
        //    else
        //    {
        //        MainWindow.IfPanel.Player2Message = posIdx.ToString();
        //    }
        //    System.Diagnostics.Debug.WriteLine(String.Format("AfterPlaceStone : {0}",
        //        _playerManager.GetCurrentPlayer().Name));
        //    ChangePlayer();
        //    if (_playerManager.GetCurrentPlayer().PlayerKind == PlayerKind.Com)
        //    {
        //        DoTurnCom(_playerManager.GetCurrentPlayer());
        //    }
        //}

        //private void DoTurnCom(Player player)
        //{
        //    var pos = player.ChoicePlacePos();
        //    if (pos != null)
        //    {
        //        PlaceStone((System.Drawing.Point)pos);
        //    }
        //}

    }
}
