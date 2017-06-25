using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
             sendRequest(new Request("ConnectPins", new object[] { (int)x, (int)y }));
         }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void resetMux_Click(object sender, RoutedEventArgs e)
        {
            sendRequest(new Request("ResetMux", 0));
        }

        private void availableHI_Click(object sender, RoutedEventArgs e)
        {
            sendRequest(new Request("GetAvailableHI", 0));
        }

        private void setHI_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem a = (ComboBoxItem)availableHIList.Items.GetItemAt(availableHIList.SelectedIndex);
            string model = a.Content.ToString();
            string family = a.Name;
            sendRequest(new Request("SetHI", new Object[] { family, model  }));
        }
    }
}
