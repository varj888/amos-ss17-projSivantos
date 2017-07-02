using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    public partial class Operation
    {


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

        /// <summary>
        /// Method to wrap updating the LCD with fixed information.
        /// </summary>
        public void updateLCD()
        {
            if (RasPi.isTestMode()) return;

            LCD.resetLCD();
            this.setLCDBackgroundState(0x01);

            string ip = GetIpAddressAsync();
            //string hi = StorageCfgs.Hi.Model;
            //string currentReceiver = StorageCfgs.Hi.CurrentReceiver;
            //string status = (RasPi.isInitialized()) ? "On" : "Off";
            //string vbat = ADConverter.getDACVoltage1().ToString();
            //string isConnected = (RasPi.skeleton.getClientCount() != 0) ? "Con" : "X";
            //string print = ip + " " + isConnected + " " + currentReceiver + " " + status + " " + vbat + "V " + hi;

            List<string> status = new List<string>
            {
                "HiModel: " +StorageCfgs.Hi.Model,
                "Receiver : " + StorageCfgs.Hi.CurrentReceiver,
                (RasPi.isInitialized()) ? "RasPi On" : "RasPi Off",
                "Volt: " +ADConverter.getDACVoltage1().ToString(),
                (RasPi.skeleton.getClientCount() != 0) ? "Con" : "X",
            };

            System.Diagnostics.Debug.WriteLine("\n Prepair Writing asynch on LCD... \n");
            Task.Run(() => print(ip, status));
        }

        public async Task print(string ip, List<string> status)
        {

            foreach (string statu in status)
            {
                LCD.clrscr();
                LCD.printInTwoLines(ip, statu);
                System.Diagnostics.Debug.WriteLine("Writing on LCD: \n {0} \n {1} \n", ip, statu);
                Task.Delay(5000).Wait();
            }

            System.Diagnostics.Debug.WriteLine("Writing on LCD: \n {0} \n {1} \n", ip, StorageCfgs.Hi.Model);
            LCD.printInTwoLines(ip, StorageCfgs.Hi.Model);
        }
    }
}
