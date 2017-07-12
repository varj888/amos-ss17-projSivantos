using CommonFiles.RPCInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestmachineFrontend
{
    public class TestCallee: IEventReceiver
    {
        public void testCall(string parameter)
        {
            Debug.WriteLine(parameter);
        }
    }
}
