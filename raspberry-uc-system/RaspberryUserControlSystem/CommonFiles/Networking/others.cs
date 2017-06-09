using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using Windows.Networking;
using Windows.Networking.Connectivity;

namespace CommonFiles.Networking
{
    public class Others
    {
        public static string GetIpAddress()
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

        public static string getHostname()
        {
            var hostNames = NetworkInformation.GetHostNames();
            return hostNames.FirstOrDefault().CanonicalName;
        }
    }
}