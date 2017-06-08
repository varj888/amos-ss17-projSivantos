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

        //initialization of each Hardware Component
        private void initializeHWComponents()
        {
            if (!testMode)
            {
                foreach (HWComponent hwcomponent in _hwComponents.Values)
                {
                    Task.Delay(250).Wait();
                    hwcomponent.initiate();
                }

                _initialized = true;

                LCD.prints(this.GetIpAddressAsync());
                Multiplexer.setReset(GPIOinterface.getPin(18));
            }
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
}
