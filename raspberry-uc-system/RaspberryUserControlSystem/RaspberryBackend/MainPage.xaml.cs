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

        /** FOR ACTUALLY CONNECTED BREADBOARD _PRODUCTION NEEDS TO BE TRUE **/
        private const Boolean _PRODUCTION = true;

        RequestController requestController = null;

        public MainPage()
        {

            // set up the gpio interface
            GPIOinterface gpiointerface = new GPIOinterface();

            // set up request controller
            requestController = RequestController.Instance;

            if (_PRODUCTION)
            {
                //initialize hardware pins(can fail if no gpio stuff is connected)
                try
                {
                    gpiointerface.initPins();
                }
                catch (Exception e)
                {
                    Debug.Fail(e.Message);

                    //TODO: Does not work (??)
                    Application.Current.Exit();
                }
            }
           

            //set the (inititialized) gpio Interface
            requestController.GpioInterface = gpiointerface;

            //Start listening for incoming requests
            runRequestServerAsync();

            this.InitializeComponent();
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
                Debug.WriteLine("Awaiting Request...");

                //Receive a Request from the client
                Request request = conn.receiveObject();

                Debug.WriteLine(string.Format("Received Request with content : (command= {0}) and (paramater= {1}) \n", request.command, request.parameter));

                //Process Request
                try
                {
                    requestController.handleRequest(request);
                }catch(ArgumentNullException e)
                {
                    Debug.Write(e.Message);
                }catch (Exception e)
                {
                    Debug.Write("Something went wrong handling the Request :( " + e.Message);
                }

            }
        }
    }
}