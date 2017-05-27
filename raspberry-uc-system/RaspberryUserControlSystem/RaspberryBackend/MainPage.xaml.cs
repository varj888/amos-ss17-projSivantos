﻿using CommonFiles.Networking;
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

        RequestController requestController = null;
        RaspberryPi raspberryPi = null;

        public MainPage()
        {


            // set up the RaspberryPi
            raspberryPi = RaspberryPi.Instance;

            // initialize Pi e.g. initialize() for default or customize it for test purposes with initialize(components) 
            raspberryPi.initialize();

            //raspberryPi.reconfigure(new GPIOinterface(), new LCD(), new Potentiometer());


            // set up request controller
            requestController = RequestController.Instance;


           
            raspberryPi.GpioInterface.initPins();
              

            //set the (inititialized) raspberryPi
            requestController.raspberryPi = raspberryPi;


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
                    Debug.WriteLine("Network error: " + e.Message);
                }
            }
        }

        private void handleRequestConnection(ObjConn<Request> conn)
        {
            while (true)
            {
                //Receive a Request from the client
                Debug.WriteLine("Awaiting Request...");
                Request request = conn.receiveObject();
                Debug.WriteLine(string.Format("Received Request with content : (command= {0}) and (paramater= {1})", request.command, request.parameter));

                //Process Request
                try
                {
                    requestController.handleRequest(request);
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error handling Request: " + e.Message);
                    //todo: notify client about error
                }
            }
        }
    }
}