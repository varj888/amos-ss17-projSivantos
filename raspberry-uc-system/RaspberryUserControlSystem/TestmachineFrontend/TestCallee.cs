using CommonFiles.RPCInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestmachineFrontend
{
    class TestCallee: IEventReceiver
    {
        public void testCall(string parameter)
        {
            Debug.WriteLine(parameter);
        }
    }
}
