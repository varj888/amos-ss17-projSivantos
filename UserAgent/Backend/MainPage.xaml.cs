using CommonFiles.Networking;
using RaspberryBackend.Components;
using System;
using System.Diagnostics;
using System.Net;
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
        IRaspberryPiOperations operations = null;
        BackChannel backChannel;

        public MainPage()
        {
            // set up the RaspberryPi
            raspberryPi = RaspberryPi.Instance;

            // try catch, because i have exception in function pulseEnable
            try
            {
                // initialize Pi e.g. initialize() for default or customize it for test purposes with initialize(components)
                raspberryPi.initialize();
                //operations = new TestOperations();
                operations = raspberryPi.Control;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Something went wrong during the initialization process of the RasPi : " + e.Message);
            }

            backChannel = new BackChannel();
            serverLoop();

            this.InitializeComponent();
        }

        private async Task serverLoop()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 54321);
            while (true)
            {
                listener.Start(1);

                try
                {
                    //await register("available");
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error registering at the registryServer :" + e.Message);
                }

                TcpClient clientSocket;
                try
                {
                     clientSocket = await listener.AcceptTcpClientAsync();
                }catch(Exception e)
                {
                    Debug.WriteLine("Error Accepting Connection " + e.Message);
                    listener.Stop();
                    continue;
                }
               
                listener.Stop();

                try
                {
                    //await register("connected");
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error registering at the registryServer :" + e.Message);
                }

                backChannel.setClient(clientSocket);

                //if the raspberry pi could not be created, operations will be used
                if (operations == null)
                {
                    RequestHandler.runRequestHandlerLoop(raspberryPi.Control, backChannel, clientSocket);
                }
                else
                {
                    RequestHandler.runRequestHandlerLoop(operations, backChannel, clientSocket);
                }

                clientSocket.Dispose();
            }
        }

        async Task register(string status)
        {
            //TcpClient registryServerSocket = new TcpClient();
            //await registryServerSocket.ConnectAsync("MarcoPC", 54320);
            //Request request = new Request("register", new object[] { "MarcoPC", status });
            //Transfer.sendObject(registryServerSocket.GetStream(), request);
            //registryServerSocket.Dispose();
        }
    }
}