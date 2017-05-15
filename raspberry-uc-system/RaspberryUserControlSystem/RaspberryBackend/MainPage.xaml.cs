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
            createListenerAsync();

            //Test for the RequestHandler
            //RequestController.handleRequest(new Request("LightLED", 1));

        }

        /// <summary>
        /// creates TCP socket that is listening on port 7777 for requests
        /// requests will be handeled by SocketListener_ConnectionReceived
        /// </summary>
        private async Task createListenerAsync()
        {

            //Create a StreamSocketListener to start listening for TCP connections.
            Windows.Networking.Sockets.StreamSocketListener socketListener = new Windows.Networking.Sockets.StreamSocketListener();

            //Hook up an event handler to call when connections are received.
            socketListener.ConnectionReceived += SocketListener_ConnectionReceived;
            Debug.WriteLine("create Listener");

            try
            {
                //Start listening for incoming TCP connections on the specified port
                await socketListener.BindServiceNameAsync("13370");
                Debug.WriteLine("created Listener");
            }
            catch (Exception e)
            {
                //Handle exception.
            }
        }

        /// <summary>
        /// 1. reads a string from the socket
        /// 2. prints the string on debug
        /// 3. deserializes the string into an object of type Request
        /// 4. prints variables of the request object on debug
        /// </summary>
        private async void SocketListener_ConnectionReceived(Windows.Networking.Sockets.StreamSocketListener sender,
            Windows.Networking.Sockets.StreamSocketListenerConnectionReceivedEventArgs args)
        {



            //Read line from the remote client.
            Stream inStream = args.Socket.InputStream.AsStreamForRead();
            StreamReader reader = new StreamReader(inStream);
            string request = await reader.ReadLineAsync();

            Debug.WriteLine(request);

            //Deserialize the received string into an object of Type Request
            Request r = (Request)Serializer.Deserialize(request, typeof(Request));

            Debug.WriteLine(r.command);
            Debug.WriteLine(r.parameter);

            //Process Request
            RequestController.Instance.handleRequest(r);

        }
    }
}