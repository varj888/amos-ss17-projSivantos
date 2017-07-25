using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegistryServer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, Tuple<bool, string>> registeredDevices;

        public MainWindow()
        {
            InitializeComponent();

            registeredDevices = new Dictionary<string, Tuple<bool, string>>();
            Task.Run(() => EmptyDictionaryLoopAsync());
            ServerLoopAsync();
        }

        private async Task EmptyDictionaryLoopAsync()
        {
            while (true)
            {
                lock (registeredDevices)
                {

                    registeredDevices = registeredDevices.Where(pair => pair.Value.Item1)
                                  .ToDictionary(pair => pair.Key,
                                                pair => new Tuple<bool, string>(false, pair.Value.Item2));
                    printRegisteredDevices("EmptyDictionaryLoop");
                }
                await Task.Delay(5000);
            }
        }

        private async Task ServerLoopAsync()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 54320);
            while (true)
            {
                listener.Start();

                TcpClient client;
                try
                {
                    client = await listener.AcceptTcpClientAsync();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error Accepting Connection " + e.Message);
                    listener.Stop();
                    continue;
                }

                Request request;
                try
                {
                    request = (Request)Transfer.receiveObject(client.GetStream());
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error receiving Request: " + e.Message);
                    continue;
                }

                IPEndPoint endpoint;
                try
                {
                    endpoint = (IPEndPoint)client.Client.RemoteEndPoint;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error casting to IPEndpoint: " + e.Message);
                    continue;
                }

                Object result;
                try
                {
                    result = new SuccessResult(handleRequest(endpoint.Address.ToString(), request));
                }
                catch(Exception e)
                {
                    result = new ExceptionResult(e.Message);
                }

                try
                {
                    Transfer.sendObject(client.GetStream(), result);
                }catch(Exception e)
                {
                    Debug.WriteLine("Error sending Result: " + e.Message);
                }

                client.Dispose();
            }
        }

        private Object handleRequest(string clientAddress, Request request)
        {
            if (request.command == "register")
            {
                register(clientAddress, (string)request.parameters[0]);
                return null;
            }
            else if (request.command == "getRegisteredDevices")
            {
                return getRegisteredDevices();
            }
            throw new NotImplementedException();
        }

        private Dictionary<string, string> getRegisteredDevices()
        {
            lock (registeredDevices)
            {
                Dictionary<string, string> result = new Dictionary<string, string>();
                foreach(var entry in registeredDevices)
                {
                    result.Add(entry.Key, entry.Value.Item2);
                }
                return result;
            }
        }

        private void register(string address, string status)
        {
            lock (registeredDevices)
            {
                registeredDevices[address] = new Tuple<bool, string>(true, status);
                printRegisteredDevices("register");
            }
        }

        private void printRegisteredDevices(string origin)
        {
            Debug.WriteLine(origin);
            foreach (var entry in registeredDevices)
            {
                Debug.WriteLine(entry.Key + ": " + registeredDevices[entry.Key].ToString());
            }
        }
    }
}
