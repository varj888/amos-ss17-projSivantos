using RaspberryBackend.Config;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace RaspberryBackend
{
    public partial class Operation
    {

        private int batterySymbolPosition = 32;

        /// <summary>
        /// Set state for background in LCD. Will want to switch to toggle
        /// </summary>
        /// <param name="targetState"></param>
        private void setLCDBackgroundState(byte targetState)
        {
            LCD.backLight = targetState;
            LCD.write(targetState, 0);
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

        private byte[] getBatterySymbol()
        {
            double batstatus = this.ADConverter.CurrentDACVoltage1 / this.ADConverter.getMaxVoltage();
            byte[] data = SymbolConfig.batterySymbol;

            for (int i = 1; i <= 6; i++)
            {
                int counter = 6;
                double frac = (double)i / 6.0;
                if (batstatus < frac)
                {
                    data[counter - i] = 0b00010001;
                }
                counter--;
            }

            return data;
        }

        private byte[] getInitSymbol(bool isInit)
        {
            return (isInit) ? SymbolConfig.isInitSymbol : SymbolConfig.notInitSymbol;
        }

        /// <summary>
        /// Method to wrap updating the LCD with fixed information.
        /// </summary>
        public void updateLCD()
        {
            if (RasPi.isTestMode()) return;

            if (!this.LCD.isShifting())
            {
                this.LCD.startShifting();
            }

            LCD.resetLCD();
            this.setLCDBackgroundState(0x01);

            string ip = GetIpAddressAsync();

            string hi = StorageCfgs.Hi.Model;
            string currentReceiver = StorageCfgs.Hi.CurrentReceiver;
            string print = ip + " " + currentReceiver + " " + hi;

            this.LCD.createSymbol(this.getBatterySymbol(), SymbolConfig.batterySymbolAddress);
            this.LCD.createSymbol(this.getInitSymbol(RasPi.isInitialized()), SymbolConfig.initSymbolAddress);
            this.LCD.printInTwoLines(print);
            this.LCD.printSymbol(SymbolConfig.batterySymbolAddress);
            this.LCD.printSymbol(SymbolConfig.initSymbolAddress);
        }
    }
}
