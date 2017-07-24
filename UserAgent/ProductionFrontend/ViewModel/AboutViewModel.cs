using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.Helpers;

namespace Frontend.ViewModel
{
    class AboutViewModel : ObservableObject
    {
        public string Version
        {
            get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }
    }
}
