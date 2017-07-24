using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Frontend.ViewModel;

namespace Frontend.Helpers
{
    /// <summary>
    /// class implementing INotifyPropertyChanged
    /// purpose: easily notifications about the updates 
    /// from interface to viewmodel or from model to viewmodel
    /// </summary>
    public abstract class ObservableObject : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
