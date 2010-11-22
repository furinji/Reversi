using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;

namespace ReversiWpf
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        protected readonly Dictionary<string, string> Errors = new Dictionary<string, string>();

        public string this[string propertyName]
        {
            get { return (Errors.ContainsKey(propertyName) ? Errors[propertyName] : null); }
        }

        public virtual string Error { get { return null; } }

        public bool HasErrors { get { return Errors.Count != 0 || !string.IsNullOrEmpty(Error); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}