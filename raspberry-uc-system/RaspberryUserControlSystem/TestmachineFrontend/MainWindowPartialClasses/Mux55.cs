using CommonFiles.TransferObjects;
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
                y = value;
                OnPropertyChanged("ValueY");
            }
        }

        private void setPinsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.addMessage("debug", x.ToString());
                this.addMessage("debug", y.ToString());
                getClientconnection().sendObject(new Request("ConnectPins", ((int)x) + "%" + ((int)y)));
            } catch(Exception ex)
            {
                this.addMessage("Debug", ex.Message);
            }
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
