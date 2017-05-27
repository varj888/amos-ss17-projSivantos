using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestmachineFrontend
{
    public partial class MainWindow : Window
    {
        public void connectToBackend()
        {
            try
            {
                clientConnection = new ClientConn<Request>(IPaddress, 13370);
                this.addMessage("TCP", "Connection to " + IPaddress + " established.");
            }
            catch (Exception e)
            {
                this.addMessage("TCP", e.Message);
            }
        }

        public void addMessage(string origin, string msg)
        {
            this.debug.Items.Insert(0, new DebugContent { origin = origin, text = msg });
        }
    }
}
