using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

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
            if (isRaspiSelected() == false)
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
            }
            catch (Exception e)
            {
                this.addMessage(request.command, "Result could not be received: " + e.Message);
                return;
            }
            processResult(request, result);
        }

        private Boolean isRaspiSelected()
        {
            return BackendList.SelectedItems.Count == 1;
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
                this.addMessage(request.command, "Failed: " + result.exceptionMessage);
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
                    this.addMessage(result.obj, "Success: " + result.value.ToString());
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
                    duration = 150;
                    break;
                case "Medium":
                    duration = 2000;
                    break;
                case "Long":
                    duration = 3500;
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

        private Dictionary<string, List<string>> buildDictionary(string xml)
        {
            XDocument config = XDocument.Parse(xml);
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();

            IEnumerable<XNode> familyNodes = config.Element("PinOutInfo").Nodes();
            foreach (XElement familyElement in familyNodes)
            {
                IEnumerable<XNode> modelNodes = familyElement.Nodes();
                string family = familyElement.Attribute("name").Value;
                foreach (XElement modelElement in modelNodes)
                {
                    List<string> models = new List<string>();
                    models.AddRange(modelElement.Attribute("name").Value.Split(','));
                    if(ret.ContainsKey(family))
                    {
                        ret[family].AddRange(models);
                    } else
                    {
                        ret[family] = models;
                    }
                }
            }
            return ret;
        }
    }
}
