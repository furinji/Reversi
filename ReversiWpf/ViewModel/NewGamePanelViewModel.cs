using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;

namespace ReversiWpf
{
    public class NewGamePanelViewModel : INotifyPropertyChanged
    {
        public NewGamePanelViewModel(NewGamePanelView newGamePanelView)
        {
            this.NewGamePanelView = newGamePanelView;
            IsActive = false;
            newGamePanelView.DataContext = this;
            PanelVisible = System.Windows.Visibility.Hidden;
            Player1Name = "あなた";
            Player1Kind = PlayerKind.User;
            Player2Name = "PC";
            Player2Kind = PlayerKind.Com;
        }

        public NewGamePanelView NewGamePanelView { get; private set; }
        public bool IsActive { get; set; }

        #region バインド

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

        private PlayerKind _player1Kind;
        public PlayerKind Player1Kind
        {
            get { return _player1Kind; }
            set
            {
                if (_player1Kind == value) { return; }
                _player1Kind = value;
                OnPropertyChanged("Player1Kind");
            }
        }

        private PlayerKind _player2Kind;
        public PlayerKind Player2Kind
        {
            get { return _player2Kind; }
            set
            {
                if (_player2Kind == value) { return; }
                _player2Kind = value;
                OnPropertyChanged("Player2Kind");
            }
        }

        private System.Windows.Visibility _panelVisible;
        public System.Windows.Visibility PanelVisible
        {
            get { return _panelVisible; }
            set
            {
                if (_panelVisible == value) { return; }
                _panelVisible = value;
                OnPropertyChanged("PanelVisible");
            }
        }

        #endregion //バインド

        #region INotifyPropertyChanged メンバ

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region コマンド

        private ICommand _closeBtnClickCommand;
        public ICommand CloseBtnClickCommand
        {
            get
            {
                if (_closeBtnClickCommand != null) return _closeBtnClickCommand;
                _closeBtnClickCommand = new DelegateCommand(
                    OnCloseBtnClick, param => true);
                return _closeBtnClickCommand;
            }
        }

        private ICommand _changeOrderBtnClickCommand;
        public ICommand ChangeOrderBtnClickCommand
        {
            get
            {
                if (_changeOrderBtnClickCommand != null) return _changeOrderBtnClickCommand;
                _changeOrderBtnClickCommand = new DelegateCommand(
                    OnChangeOrderBtnClick, param => true);
                return _changeOrderBtnClickCommand;
            }
        }

        #endregion //コマンド

        private void OnCloseBtnClick(object param)
        {
            IsActive = false;
        }

        private void OnChangeOrderBtnClick(object param)
        {
            var tpName = Player1Name;
            Player1Name = Player2Name;
            Player2Name = tpName;
            var tpPlayerKind = Player1Kind;
            Player1Kind = Player2Kind;
            Player2Kind = tpPlayerKind;
        }

    }
}
