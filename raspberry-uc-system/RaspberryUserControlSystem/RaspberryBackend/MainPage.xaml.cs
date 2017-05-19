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
using System.Runtime.Serialization;
using System.Xml;
using Windows.Networking.Sockets;

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
            TCPServer<Request> RequestServer = new TCPServer<Request>(13370, handleRequestConnection);

        }

        private void handleRequestConnection(ObjConn<Request> conn)
        {
            try
            {
                while (true)
                {
                    //Receive a Request from the client
                    Request request = conn.receiveObject();

                    Debug.WriteLine(string.Format("Received Request with content : command= {0} and paramater= {1}", request.command, request.parameter) + "/n");


                    //Process Request
                    RequestController.Instance.handleRequest(request);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("HandleRequestConnection failed" + e.Message);
                return;
            }
        }
    }
}