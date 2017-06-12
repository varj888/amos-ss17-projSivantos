using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Diagnostics;
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
        RequestController requestController = null;
        RaspberryPi raspberryPi = null;

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
                Debug.WriteLine("Something went wrong during the initialization process of the RasPi : "+e.Message);
            }

            // set up request controller
            requestController = RequestController.Instance;

            //set the (inititialized) raspberryPi
            requestController.raspberryPi = raspberryPi;

            //register at the registry server
            registerAsync();

            //Start listening for incoming requests
            runRequestServerAsync();

            this.InitializeComponent();
        }

        private async Task runRequestServerAsync()
        {
            TCPServer<Request, Result> requestServer = new TCPServer<Request, Result>(54321);
            while (true)
            {
                try
                {
                    Debug.WriteLine(this.GetType().Name + "::: Awaiting request...");
                    using (ObjConn<Request, Result> connection = await requestServer.acceptConnectionAsync())
                    {
                        handleRequestConnection(connection);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Network error: " + e.Message);
                }
            }
        }

        private void handleRequestConnection(ObjConn<Request, Result> conn)
        {
            while (true)
            {
                //Receive a Request from the client
                Debug.WriteLine("Awaiting Request...");
                Request request = conn.receiveObject();
                Debug.WriteLine(string.Format("Received Request with content : (command= {0}) and (paramater= {1})", request.command, request.parameter));

                //Process Request
                Result result = requestController.handleRequest(request);

                //Send back Result to the client
                conn.sendObject(result);
            }
        }

        private async Task registerAsync()
        {
            ClientConn<Result, Request> conn = await ClientConn<Result, Request>.connectAsync("MarcoPC", 54320);
            string[] values = new string[] { Others.getHostname(), Others.GetIpAddress() };
            conn.sendObject(new Request("register", values));
        }
    }
}