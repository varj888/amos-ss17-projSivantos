using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMachineFrontend1.Helpers;

namespace TestMachineFrontend1.ViewModel
{
    public class MultiplexerViewModel : ObservableObject
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
    }
}
