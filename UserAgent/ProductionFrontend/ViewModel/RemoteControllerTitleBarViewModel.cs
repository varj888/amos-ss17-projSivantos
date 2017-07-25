using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestMachineFrontend1.Helpers;

namespace TestMachineFrontend1.ViewModel
{
    public class RemoteControllerTitleBarViewModel : ObservableObject
    {
        private Visibility _ToggleMenuButton_Off_Visibility = Visibility.Visible;
        public Visibility ToggleMenuButton_Off_Visibility
        {
            get { return _ToggleMenuButton_Off_Visibility; }
            set
            {
                _ToggleMenuButton_Off_Visibility = value;
                OnPropertyChanged("ToggleMenuButton_Off_Visibility");
            }
        }

        private Visibility _ToggleMenuButton_On_Visibility = Visibility.Hidden;
        public Visibility ToggleMenuButton_On_Visibility
        {
            get { return _ToggleMenuButton_On_Visibility; }
            set
            {
                _ToggleMenuButton_On_Visibility = value;
                OnPropertyChanged("ToggleMenuButton_On_Visibility");
            }
        }
    }
}
