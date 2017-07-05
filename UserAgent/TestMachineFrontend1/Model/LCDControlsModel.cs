using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMachineFrontend1.Helpers;

namespace TestMachineFrontend1.Model
{
    public class LCDControlsModel : ObservableObject
    {
        private int lcdBacklightState = 0;
        public int LcdBacklightState
        {
            get; private set;
        }
    }
}
