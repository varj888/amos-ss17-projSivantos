using CommonFiles.Networking;
using RaspberryBackend.Components;
using System;
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
        BackChannel backChannel;
        RequestHandler requestHandler;

        public MainPage()
        {
            // set up the RaspberryPi
            raspberryPi = RaspberryPi.Instance;

            // try catch, because i have exception in function pulseEnable
            try
            {
                // initialize Pi e.g. initialize() for default or customize it for test purposes with initialize(components)
                raspberryPi.initialize();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Something went wrong during the initialization process of the RasPi : " + e.Message);
            }

            //register at the registry server
            //registerAsync();

<<<<<<< HEAD
            //ServerSkeleton raspberryPiSkeleton = new ServerSkeleton(raspberryPi, 54321);
            //raspberryPi.setSkeleton(raspberryPiSkeleton);
            init();
=======
            // set up the skeleton
            runServerStubsAsync();

            ServerSkeleton raspberryPiSkeleton = new ServerSkeleton(raspberryPi.Control, 54321);
            raspberryPi.setSkeleton(raspberryPiSkeleton);
>>>>>>> refs/remotes/origin/master

            this.InitializeComponent();
        }

        async void init()
        {
<<<<<<< HEAD
            backChannel = new BackChannel();
            requestHandler = new RequestHandler();

            TCPServer server = new TCPServer(54321);
            server.connectionAccepted += handleConnection;
            await server.runServerLoop();
=======
            while (true)
            {
                try
                {
                    ServerStub stub;
                    using (stub = await ServerStub.createServerStubAsync(54322))
                    {
                        await handleServerStubAsync(stub);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("error in runServerStub Loop: " + e.Message);
                }
            }
>>>>>>> refs/remotes/origin/master
        }

        private void handleConnection(Object sender, TcpClient socket)
        {
            backChannel.setClient(socket);
            RequestHandler.runRequestHandlerLoop(raspberryPi, backChannel, socket);
        }

       

        //private async Task registerAsync()
        //{
        //    ClientConn<Result, Request> conn = await ClientConn<Result, Request>.connectAsync("MarcoPC", 54320);
        //    string[] values = new string[] { Others.getHostname(), Others.GetIpAddress() };
        //    conn.sendObject(new Request("register", values));
        //}
    }
}