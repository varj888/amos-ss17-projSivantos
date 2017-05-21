using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Xml;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RaspberryBackend
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {

            this.InitializeComponent();
            runRequestServerAsync();

        }

        private async Task runRequestServerAsync()
        {
            TCPServer<Request> requestServer = new TCPServer<Request>(13370);
            while (true)
            {
                try
                {
                    using (ObjConn<Request> connection = await requestServer.acceptConnectionAsync())
                    {
                        handleRequestConnection(connection);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("HandleRequestConnection failed" + e.Message);
                }

            }
        }

        private void handleRequestConnection(ObjConn<Request> conn)
        {
            while (true)
            {
                //Receive a Request from the client
                Request request = conn.receiveObject();

                Debug.WriteLine(string.Format("Received Request with content : (command= {0}) and (paramater= {1}) \n", request.command, request.parameter));

                //Process Request
                //RequestController.Instance.handleRequest(request);
            }
        }
    }
}