using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    public partial class RaspberryPi
    {
        public GPIOinterface GPIOinterface { get => _initialized ? (GPIOinterface)_hwComponents[typeof(GPIOinterface).Name] : null; }
        public LCD LCD { get => _initialized ? (LCD)_hwComponents[typeof(LCD).Name] : null; }
        public Potentiometer Potentiometer { get => _initialized ? (Potentiometer)_hwComponents[typeof(Potentiometer).Name] : null; }
        public Multiplexer Multiplexer { get => _initialized ? (Multiplexer)_hwComponents[typeof(Multiplexer).Name] : null; }
        public ADCDAC ADCDAC { get => _initialized ? (ADCDAC)_hwComponents[typeof(ADCDAC).Name] : null; }


        private void initializeHWComponents()
        {
            foreach (HWComponent hwcomponent in _hwComponents.Values)
            {
                Task.Delay(500).Wait();
                hwcomponent.initiate();
            }

            LCD.prints(this.GetIpAddressAsync());
        }

        private string GetIpAddressAsync()
        {
            var ipAsString = "Not Found";
            var hosts = Windows.Networking.Connectivity.NetworkInformation.GetHostNames().ToList();
            var hostNames = new List<string>();

            //NetworkInterfaceType
            foreach (var h in hosts)
            {
                hostNames.Add(h.DisplayName);
                if (h.Type == Windows.Networking.HostNameType.Ipv4)
                {
                    var networkAdapter = h.IPInformation.NetworkAdapter;
                    if (networkAdapter.IanaInterfaceType == (uint)NetworkInterfaceType.Ethernet || networkAdapter.IanaInterfaceType == (uint)NetworkInterfaceType.Wireless80211)
                    {
                        IPAddress ip;
                        if (!IPAddress.TryParse(h.DisplayName, out ip)) continue;
                        if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) return ip.ToString();
                    }

                }
            }
            return ipAsString;
        }
    }




    //===================================================================================================================
    //======================= Different Approach (untestet) with hwComponents as a List ==================================
    //===================================================================================================================

    //public GPIOinterface GPIOinterface { get => (GPIOinterface)getHwComponent(typeof(GPIOinterface).GetType()); }
    //public LCD LCD { get => (LCD)getHwComponent(typeof(LCD).GetType()); }
    //public Potentiometer Potentiometer { get => (Potentiometer)getHwComponent(typeof(Potentiometer).GetType()); }
    //public Multiplexer Multiplexer { get => (Multiplexer)getHwComponent(typeof(Multiplexer).GetType()); }
    //public ADCDAC ADCDAC { get => (ADCDAC)getHwComponent(typeof(ADCDAC).GetType()); }


    ///// <summary>
    ///// Finds a specific Hardware Component in the List hwcomponents
    ///// </summary>
    ///// <param name="HWcomponentType"></param>
    ///// <returns></returns>
    //private HWComponent getHwComponent(Type HWcomponentType)
    //{
    //    foreach (HWComponent hwComponent in hwComponents.Values)
    //    {
    //        if (typeof(HWComponent).GetType().Equals(hwComponent.GetType()))
    //        {
    //            return hwComponent;
    //        }
    //    }

    //    throw new TypeLoadException("Hardware Component could not be found");
    //}
}
