using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryBackend.Operations
{
    public class ConnectPins : IOperation
    {
        private RaspberryPi raspberry;

        public ConnectPins(RaspberryPi raspberry)
        {
            this.raspberry = raspberry;
        }

        public void doOperation()
        {
           
        }
    }
}
