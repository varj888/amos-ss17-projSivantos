using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.Helpers;

namespace Frontend.Model
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
