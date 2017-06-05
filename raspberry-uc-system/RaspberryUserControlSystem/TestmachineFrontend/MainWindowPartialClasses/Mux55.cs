using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestmachineFrontend
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private double x;
        public double ValueX
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged("ValueX");
            }
        }
        private double y;
        public double ValueY
        {
            get { return y; }
            set
            {
                x = value;
                OnPropertyChanged("ValueY");
            }
        }

        private void setPinsButton_Click(object sender, RoutedEventArgs e)
        {
            //Not yet implemented
            //y = 1.0;
            //valueY.Text = ValueY.ToString();
        }

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
