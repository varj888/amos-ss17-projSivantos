using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestmachineFrontend
{
    class TestCallee
    {
        public void testCall(string parameter)
        {
            Debug.WriteLine(parameter);
        }
    }
}
