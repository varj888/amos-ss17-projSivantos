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
        private List<string[]> registeredDevices;

        public RegistryService()
        {
            registeredDevices = new List<string[]>();
        }

        public void register(Object argument)
        {
            string[] values = (string[])argument;
            registeredDevices.Add(values);
            Debug.WriteLine(values[0]);   
            Debug.WriteLine(values[1]);
        }
    }
}
