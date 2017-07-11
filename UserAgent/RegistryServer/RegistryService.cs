using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistryServer
{
    public class RegistryService
    {
        private Dictionary<string, string> registeredDevices;

        public RegistryService()
        {
            registeredDevices = new Dictionary<string, string>();
        }

        public void register(string hostname, string status)
        {
            registeredDevices[hostname] = status;
            Debug.WriteLine(hostname);
            Debug.WriteLine(status);
        }

        public Dictionary<string, string> getRegisteredDevices()
        {
            return registeredDevices;
        }
    }
}
