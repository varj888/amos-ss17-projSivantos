using CommonFiles.Networking;
using RaspberryBackend.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RaspberryBackend
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        RaspberryPi raspberryPi = null;
        TestOperations testOperations = null;
        BackChannel backChannel;

        public MainPage()
        {
            // set up the RaspberryPi
            raspberryPi = RaspberryPi.Instance;

            try
            {
                // initialize Pi e.g. initialize() for default or customize it for test purposes with initialize(components)
                raspberryPi.initialize();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Something went wrong during the initialization process of the RasPi : " + e.Message);
                testOperations = new TestOperations();
            }

            //register at the registry server
            //registerAsync();

            init();

            this.InitializeComponent();
        }

        async void init()
        {
            backChannel = new BackChannel();

            TCPServer server = new TCPServer(54321);
            server.connectionAccepted += handleConnection;
            await server.runServerLoop();
        }

        private void handleConnection(Object sender, TcpClient socket)
        {
            backChannel.setClient(socket);

            //if the raspberry pi could not be created, testOperations will be used
            if(testOperations == null)
            {
                RequestHandler.runRequestHandlerLoop(raspberryPi.Control, backChannel, socket);
            }
            else
            {
                RequestHandler.runRequestHandlerLoop(testOperations, backChannel, socket);
            }
        }

       

        //private async Task registerAsync()
        //{
        //    ClientConn<Result, Request> conn = await ClientConn<Result, Request>.connectAsync("MarcoPC", 54320);
        //    string[] values = new string[] { Others.getHostname(), Others.GetIpAddress() };
        //    conn.sendObject(new Request("register", values));
        //}
    }
}