using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend;
using Frontend.Helpers;

namespace Frontend.Model

{
    public class RaspberryPiItem : ObservableObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Status { get; set; }
        public RaspberryPi raspi { get; set; }
        public UInt16 PinID { get; set; }
        public string IPaddress { get; set; }
    }
}
