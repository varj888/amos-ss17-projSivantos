using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace TestmachineFrontend
{
    public partial class MainWindow : Window
    {
        //Creates one time a List with all public methods in MainWindow
        public List<string> mainMethods = getMethodNamesOf(typeof(MainWindow));

        /// <summary>
        /// Adds a message to the GUI table
        /// </summary>
        /// <param name="origin">The type/kategorie/source of the message</param>
        /// <param name="msg">Actually content of the message</param>
        public void addMessage(string origin, string msg)
        {
            this.debug.Items.Insert(0, new DebugContent { origin = origin, text = msg });
        }

        /// <summary>
        /// Sends a Request from Frontend to Backend. Uses <see cref="addMessage(string, string)"./>
        /// </summary>
        /// <param name="request">The Request for Backend</param>
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

        /// <summary>
        /// This method processes the responded result from Backend. It will also invoke the updating process of GUI elements.
        /// </summary>
        /// <param name="request">The former Request which was sent from Frontend to Backend</param>
        /// <param name="result">The responded result from Backend</param>
        public void processResult(Request request, Result result)
        {
            if (!result.success)
            {
                this.addMessage(request.command, result.exceptionMessage);
            } // Check whether we have an obj string and a appropriate value
            else if (result.success == true && result.obj != null && result.value != null)
            {
                string methodName = "updateGUI_" + request.command;

                if (mainMethods.Contains(methodName))
                {
                    MethodInfo updateCommand = typeof(MainWindow).GetMethod(methodName);
                    updateCommand.Invoke(this, new object[] { result });
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

        /// <summary>
        /// This helper Method can get the names of all public and declared instance method  of a Class
        /// </summary>
        /// <param name="type">The Type of a class. Use typeof(Class) to get the Type of a class</param>
        /// <returns>A list with strings of all names of public method within the defined type</returns>
        private static List<string> getMethodNamesOf(Type type)
        {
            MethodInfo[] methodInfo = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            string[] methodNames = methodInfo.Select(n => n.Name).ToArray();

            return new List<string>(methodNames);
        }
    }
}
