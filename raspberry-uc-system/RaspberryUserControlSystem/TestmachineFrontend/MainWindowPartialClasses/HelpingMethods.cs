using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Windows;
using System.Windows.Controls;

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
            if (BackendList.SelectedItems.Count != 1)
            {
                this.addMessage("Debug", "No raspi selected");
                return;
            }

            try
            {
                getClientconnection().sendObject(request);
            }
            catch (Exception ex)
            {
                this.addMessage(request.command, "Request could not be sent: " + ex.Message);
                return;
            }

            Result result;

            try
            {
                result = getClientconnection().receiveObject();
                processResult(request, result);
            }
            catch (Exception e)
            {
                this.addMessage(request.command, "Result could not be received: " + e.Message);
                return;
            }
        }

        public void processResult(Request request, Result result)
        {
            if (!result.success)
            {
                this.addMessage(request.command, result.exceptionMessage);
            } // Check whether we have an obj string and a appropriate value
            else if (result.success == true && result.obj != null && result.value != null)
            {
                // Do something with the result object here, for now we just output it on the commandline
                if (commandMap.ContainsKey(request.command))
                {
                    commandMap[request.command].Invoke(this, new object[] { result });
                }
                else
                {
                    this.addMessage(result.obj, result.value.ToString());
                }
            }
        }

        private ClientConn<Result, Request> getClientconnection()
        {
            if (BackendList.SelectedIndex == -1 && BackendList.Items.Count > 0)
            {
                BackendList.SelectedIndex = 0;
            }
            var c = (RaspberryPiItem)BackendList.Items.GetItemAt(BackendList.SelectedIndex);
            return c.raspi.clientConnection;
        }

        private int getDuration()
        {
            if (durationBox.SelectedIndex < 0)
            {
                return -1;
            }
            var a = (ComboBoxItem)durationBox.Items.GetItemAt(durationBox.SelectedIndex);
            UInt16 duration;
            switch (a.Content)
            {
                case "Short":
                    duration = 50;
                    break;
                case "Medium":
                    duration = 500;
                    break;
                case "Long":
                    duration = 3000;
                    break;
                default:
                    return -1;
            }
            return duration;
        }

        private class RaspberryPiItem
        {
            public string Name { get; set; }
            public int Id { get; set; }
            public string Status { get; set; }
            public RaspberryPi raspi { get; set; }
        }
    }
}
