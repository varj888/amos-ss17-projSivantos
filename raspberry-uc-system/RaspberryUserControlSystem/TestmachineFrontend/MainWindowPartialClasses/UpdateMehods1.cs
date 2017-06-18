using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestmachineFrontend
{
    public partial class MainWindow : Window
    {
        public Dictionary<string, MethodInfo> commandMap = new Dictionary<string, MethodInfo>
        {
            {"Command", typeof(MainWindow).GetMethod("updateUIElement")}
        };

        public void updateUIElement(int value)
        {
            Debug.WriteLine("Found");
        }

       
    }
}
