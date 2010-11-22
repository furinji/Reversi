using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media;

namespace ReversiWpf
{
    public class InfoPanelViewModel : INotifyPropertyChanged
    {
        public InfoPanelViewModel(InfoPanelView interFaceViewPanelView)
        {
            InterfacePanelView = interFaceViewPanelView;
            _message = "";
            _message2 = "";
            Player1StoneColor = CellState.Black;
            Player2StoneColor = CellState.White;
            SetPlayer1Active(false);
            SetPlayer2Active(false);
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message == value) { return; }
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        private string _message2;
        public string Message2
        {
            get { return _message2; }
            set
            {
                if (_message2 == value) { return; }
                _message2 = value;
                OnPropertyChanged("Message2");
            }
        }

        private CellState _player1StoneColor;
        public CellState Player1StoneColor
        {
            get { return _player1StoneColor; }
            set
            {
                if (_player1StoneColor == value) { return; }
                _player1StoneColor = value;
                Player1Brush = ImageManager.GetStoneImageBrush(_player1StoneColor);
            }
        }

        private CellState _player2StoneColor;
        public CellState Player2StoneColor
        {
            get { return _player2StoneColor; }
            set
            {
                if (_player2StoneColor == value) { return; }
                _player2StoneColor = value;
                Player2Brush = ImageManager.GetStoneImageBrush(_player2StoneColor);
            }
        }

        

        private string _player1Name;
        public string Player1Name
        {
            get { return _player1Name; }
            set
            {
                if (_player1Name == value) { return; }
                _player1Name = value;
                OnPropertyChanged("Player1Name");
            }
        }

        private string _player2Name;
        public string Player2Name
        {
            get { return _player2Name; }
            set
            {
                if (_player2Name == value) { return; }
                _player2Name = value;
                OnPropertyChanged("Player2Name");
            }
        }

        private int _playerIdx;
        public int PlayerIdx
        {
            get { return _playerIdx; }
            set
            {
                if (_playerIdx == value) { return; }
                _playerIdx = value;
            }
        }

        private string _player1Message;
        public string Player1Message
        {
            get { return _player1Message; }
            set
            {
                if (_player1Message == value) { return; }
                _player1Message = value;
                OnPropertyChanged("Player1Message");
            }
        }

        private string _player2Message;
        public string Player2Message
        {
            get { return _player2Message; }
            set
            {
                if (_player2Message == value) { return; }
                _player2Message = value;
                OnPropertyChanged("Player2Message");
            }
        }

        private Brush _player1Brush;
        public Brush Player1Brush
        {
            get { return _player1Brush; }
            set
            {
                if (_player1Brush == value) { return; }
                _player1Brush = value;
                OnPropertyChanged("Player1Brush");
            }
        }

        private Brush _player2Brush;
        public Brush Player2Brush
        {
            get { return _player2Brush; }
            set
            {
                if (_player2Brush == value) { return; }
                _player2Brush = value;
                OnPropertyChanged("Player2Brush");
            }
        }

        private string _player1NumberOfStone;
        public string Player1NumberOfStone
        {
            get { return _player1NumberOfStone; }
            set
            {
                if (_player1NumberOfStone == value) { return; }
                _player1NumberOfStone = value;
                OnPropertyChanged("Player1NumberOfStone");
            }
        }

        private string _player2NumberOfStone;
        public string Player2NumberOfStone
        {
            get { return _player2NumberOfStone; }
            set
            {
                if (_player2NumberOfStone == value) { return; }
                _player2NumberOfStone = value;
                OnPropertyChanged("Player2NumberOfStone");
            }
        }

        private double _player1BackOpacity;
        public double Player1BackOpacity
        {
            get { return _player1BackOpacity; }
            set
            {
                if (_player1BackOpacity == value) { return; }
                _player1BackOpacity = value;
                OnPropertyChanged("Player1BackOpacity");
            }
        }

        private double _player2BackOpacity;
        public double Player2BackOpacity
        {
            get { return _player2BackOpacity; }
            set
            {
                if (_player2BackOpacity == value) { return; }
                _player2BackOpacity = value;
                OnPropertyChanged("Player2BackOpacity");
            }
        }

        public void SetPlayer1Active(bool isActive)
        {
            if (isActive)
            {
                Player1BackOpacity = 0.6;
            }
            else
            {
                Player1BackOpacity = 0.2;
            }
        }

        public void SetPlayer2Active(bool isActive)
        {
            if (isActive)
            {
                Player2BackOpacity = 0.6;
            }
            else
            {
                Player2BackOpacity = 0.2;
            }
        }

        public InfoPanelView InterfacePanelView { get; private set; }

        public void CountNumColor()
        {
            var numColor = MainWindow.Board.GetNumberOfColor();
            Player1NumberOfStone = numColor.Item1.ToString() + "個";
            Player2NumberOfStone = numColor.Item2.ToString() + "個";
        }

        public void SetPlayerMessage(OverturnableInfo turnInfo, int playerIdx)
        {
            string msg;
            if (turnInfo != null)
            {
                msg = new string[] { "A", "B", "C", "D", "E", "F", "G", "H" }[turnInfo.IdxX];
                msg += (turnInfo.IdxY + 1).ToString();
            }
            else
            {
                msg = "パス";
            }
            if (playerIdx == 0)
            {
                Player1Message = msg;
            }
            else
            {
                Player2Message = msg;
            }
        }

        public void SetPlayerMessage(string msg, int playerIdx)
        {
            if (playerIdx == 0)
            {
                Player1Message = msg;
            }
            else
            {
                Player2Message = msg;
            }
        }

        public void ClearPlayerMessage()
        {
            Player1Message = "";
            Player2Message = "";
        }

        #region INotifyPropertyChanged メンバ

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion


    }
}
