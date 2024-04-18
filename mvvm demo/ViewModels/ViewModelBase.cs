﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace mvvm_demo.ViewModels
{
    public abstract class ViewModelBase: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
