using CommonFiles.Networking;
using CommonFiles.TransferObjects;
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
        IOperations operations = null;
        BackChannel backChannel;
        string status;
        public static TcpClient clientSocket { get; private set; }

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
                operations = new TestOperations();
            }

            backChannel = new BackChannel();
            Task.Run(() => registerLoop());
            serverLoop();

            this.InitializeComponent();
        }

        private async Task serverLoop()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 54321);
            while (true)
            {
                status = "available";
                listener.Start(1);

                clientSocket = null;
                try
                {
                    clientSocket = await listener.AcceptTcpClientAsync();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error Accepting Connection " + e.Message);
                    listener.Stop();
                    continue;
                }

                listener.Stop();
                status = "connected";

                backChannel.setClient(clientSocket);
                RequestHandler.runRequestHandlerLoop(operations, backChannel, clientSocket);
                clientSocket.Dispose();
            }
        }

        private async Task registerLoop()
        {
            while (true)
            {
                TcpClient registryServerSocket = new TcpClient();
                try
                {
                    Request request = new Request("register", status);
                    await registryServerSocket.ConnectAsync("192.168.178.21", 54320);
                    Transfer.sendObject(registryServerSocket.GetStream(), request);
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error registering at the registryServer: " + e.Message);
                }
                registryServerSocket.Dispose();
                await Task.Delay(1000);
            }
        }
    }
}