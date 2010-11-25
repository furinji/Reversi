using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ReversiWpf
{
    public class CellViewModel : INotifyPropertyChanged
    {
        public CellViewModel()
        {
            State = CellState.None;
            EffectByMouseOver = ReversiWpf.EffectByMouseOver.None;
            BlackStoneImageBrush = ImageManager.GetBlackStoneImageBrush();
            WhiteStoneImageBrush = ImageManager.GetWhiteStoneImageBrush();
        }

        public static CellViewModel CreateInstance()
        {
            var cellViewModel = new CellViewModel();
            cellViewModel.CellView = new CellView();
            cellViewModel.CellView.DataContext = cellViewModel;
            return cellViewModel;
        }

        private System.Drawing.Point _posIdx;
        private CellState _state;
        private readonly double _defaultLayerOpacity = 0.4;
        private readonly double _defaultTemporaryStoneOpacity = 0.5;

        public CellView CellView { get; private set; }

        public System.Drawing.Point PosIdx
        {
            get { return _posIdx; }
            set
            {
                if (_posIdx == value) { return; }
                _posIdx = value;
                OnPropertyChanged("PosIdx");
            }
        }

        public Brush BlackStoneImageBrush { get; set; }
        public Brush WhiteStoneImageBrush { get; set; }

        public CellState State
        {
            get { return _state; }
            set
            {
                if (_state == value) { return; }
                _state = value;
                switch (_state)
                {
                    case CellState.Black:
                        StoneBrush = BlackStoneImageBrush;
                        StoneOpacity = 1.0;
                        break;
                    case CellState.White:
                        StoneBrush = WhiteStoneImageBrush;
                        StoneOpacity = 1.0;
                        break;
                    default:
                        StoneOpacity = 0.0;
                        break;
                }
            }
        }

        private Brush _stoneBrush;
        public Brush StoneBrush
        {
            get { return _stoneBrush; }
            set
            {
                if (_stoneBrush == value) { return; }
                _stoneBrush = value;
                OnPropertyChanged("StoneBrush");
            }
        }

        private double _stoneOpacity;
        public double StoneOpacity
        {
            get { return _stoneOpacity; }
            set
            {
                if (_stoneOpacity == value) { return; }
                _stoneOpacity = value;
                OnPropertyChanged("StoneOpacity");
            }
        }

        private double _layerOpacity;
        public double LayerOpacity
        {
            get { return _layerOpacity; }
            set
            {
                if (_layerOpacity == value) { return; }
                _layerOpacity = value;
                OnPropertyChanged("LayerOpacity");
            }
        }

        private EffectByMouseOver _effectByMouseOver;
        public EffectByMouseOver EffectByMouseOver
        {
            get { return _effectByMouseOver; }
            set
            {
                if (_effectByMouseOver == value) { return; }
                _effectByMouseOver = value;
                switch (_effectByMouseOver)
                {
                    case EffectByMouseOver.MouseOver:
                        if (State == CellState.None)
                        {
                            StoneOpacity = 0.0;
                        }
                        else
                        {
                            StoneOpacity = 1.0;
                        }
                        LayerOpacity = _defaultLayerOpacity;
                        break;
                    case EffectByMouseOver.MouseOverAndTemporaryBlack:
                        if (State == CellState.None)
                        {
                            StoneBrush = BlackStoneImageBrush;
                            StoneOpacity = _defaultTemporaryStoneOpacity;
                        }
                        else
                        {
                            StoneOpacity = 1.0;
                        }
                        LayerOpacity = _defaultLayerOpacity;
                        break;
                    case EffectByMouseOver.MouseOverAndTemporaryWhite:
                        if (State == CellState.None)
                        {
                            StoneBrush = WhiteStoneImageBrush;
                            StoneOpacity = _defaultTemporaryStoneOpacity;
                        }
                        else
                        {
                            StoneOpacity = 1.0;
                        }
                        LayerOpacity = _defaultLayerOpacity;
                        break;
                    default:
                        if (State == CellState.None)
                        {
                            StoneOpacity = 0.0;
                        }
                        else
                        {
                            StoneOpacity = 1.0;
                        }
                        LayerOpacity = 0.0;
                        break;
                }
            }
        }

        #region INotifyPropertyChanged メンバ

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region コマンド
        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                if (_clickCommand != null) return _clickCommand; 
                _clickCommand = new DelegateCommand(
                    OnClick, param => true);
                return _clickCommand;
            }
        }
        #endregion  

        private void OnClick(object param)
        {
            Game.Instance.OnClicked(new System.Drawing.Point((int)PosIdx.X, (int)PosIdx.Y));
        }

    }
}