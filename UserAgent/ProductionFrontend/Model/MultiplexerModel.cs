using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.Helpers;

namespace Frontend.Model
{
    public class MultiplexerModel : ObservableObject
    {
        private double x;
        /// <summary>
        /// Value for x
        /// </summary>
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
        /// <summary>
        /// Value for y
        /// </summary>
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
