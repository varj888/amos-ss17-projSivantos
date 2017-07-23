using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestmachineFrontend1;
using TestMachineFrontend1.Helpers;

namespace TestMachineFrontend1.Model

{
    public class RaspberryPiItem : ObservableObject
    {
        public IPEndPoint endpoint { get; set; }
        public string Status { get; set; }
        public RaspberryPi raspi { get; set; }
    }
}
