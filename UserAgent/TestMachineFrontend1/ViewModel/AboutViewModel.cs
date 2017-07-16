using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMachineFrontend1.ViewModel
{
    class AboutViewModel
    {
        public string Version
        {
            get { return "Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }
    }
}
