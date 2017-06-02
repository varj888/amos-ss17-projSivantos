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
        public void addMessage(string origin, string msg)
        {
            this.debug.Items.Insert(0, new DebugContent { origin = origin, text = msg });
        }

        public void sendRequest(Request request)
        {
            try
            {
                clientConnection.sendObject(request);
            }
            catch (Exception ex)
            {
                this.addMessage(request.command, "Request could not be sent: " + ex.Message);
                return;
            }

            Result result;

            try
            {
                result = clientConnection.receiveObject();
            }catch(Exception e)
            {
                this.addMessage(request.command, "Result could not be received: " + e.Message);
                return;
            }

            if (result.exceptionMessage == null)
            {
                this.addMessage(request.command, "sucess");
            }
            else
            {
                this.addMessage(request.command, result.exceptionMessage);
            }
        }
    }
}
