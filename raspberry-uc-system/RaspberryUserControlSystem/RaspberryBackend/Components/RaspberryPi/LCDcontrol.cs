using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace RaspberryBackend
{
    public partial class RaspberryPi
    {
        /// <summary>
        /// Print string to LCD display
        /// </summary>
        /// <param name="s"></param>
        public void writeToLCD(string s)
        {
            LCD.clrscr();
            LCD.prints(s);
        }

        /// <summary>
        /// Print two lines to LCD
        /// </summary>
        /// <param name="s"></param>
        public void writeToLCDTwoLines(string s)
        {
            int maxCharLCD = 16;
            LCD.printInTwoLines(s, maxCharLCD);
        }

        /// <summary>
        /// Reset the LCD (clear it's screen)
        /// </summary>
        public void resetLCD()
        {
            LCD.initiateLCD();
        }

        /// <summary>
        /// Set state for background in LCD. Will want to switch to toggle
        /// </summary>
        /// <param name="targetState"></param>
        public void setLCDBackgroundState(byte targetState)
        {
            LCD.backLight = targetState;
            LCD.write(targetState, 0);
            displayIPAdressOnLCD();
        }

        private void displayIPAdressOnLCD()
        {
            writeToLCD(this.GetIpAddressAsync());
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
