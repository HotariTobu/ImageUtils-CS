using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivDiv
{
    internal class MWVM: ViewModelBase
    {
        #region == InputPath ==

        private string _InputPath;

        public string InputPath
        {
            get => _InputPath;
            set
            {
                _InputPath = value;
                RaisePropertyChanged();
            }
        }

        #endregion
        /*#region == OutputPath ==

        private string _OutputPath;

        public string OutputPath
        {
            get => _OutputPath;
            set
            {
                _OutputPath = value;
                RaisePropertyChanged();
            }
        }

        #endregion*/

        public event Action<MWVM> SelectedItemChanged;
        #region == SizeTypeItem ==

        public SizeType SizeType { get; set; }

        private object _SizeTypeItem;
        public object SizeTypeItem
        {
            get => _SizeTypeItem;
            set
            {
                _SizeTypeItem = value;
                SelectedItemChanged?.Invoke(this);
            }
        }

        #endregion
        #region == DividingModeItem ==

        public DividingMode DividingMode { get; set; }

        private object _DividingModeItem;
        public object DividingModeItem
        {
            get => _DividingModeItem;
            set
            {
                _DividingModeItem = value;
                SelectedItemChanged?.Invoke(this);
            }
        }

        #endregion

        #region == Images ==

        private readonly ObservableCollection<DividedImage> _Images = new ObservableCollection<DividedImage>();
        public ObservableCollection<DividedImage> Images => _Images;

        #endregion

        #region == MarginValue ==

        private double _MarginValue = 100;
        public double MarginValue
        {
            get => _MarginValue;
            set
            {
                if (_MarginValue != value)
                {
                    _MarginValue = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion
        #region == Quality ==

        private int _Quality = 200;
        public int Quality
        {
            get => _Quality;
            set
            {
                if (_Quality != value)
                {
                    _Quality = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region == IsLoadButtonEnabled ==

        private bool _IsLoadButtonEnabled = true;
        public bool IsLoadButtonEnabled
        {
            get => _IsLoadButtonEnabled;
            set
            {
                if (_IsLoadButtonEnabled != value)
                {
                    _IsLoadButtonEnabled = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion
        #region == IsPrintButtonEnabled ==

        private bool _IsPrintButtonEnabled = true;
        public bool IsPrintButtonEnabled
        {
            get => _IsPrintButtonEnabled;
            set
            {
                if (_IsPrintButtonEnabled != value)
                {
                    _IsPrintButtonEnabled = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion
    }
}
