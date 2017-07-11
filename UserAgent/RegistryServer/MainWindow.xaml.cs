using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        RegistryService service;
        TCPServer server;
        BackChannel backChannel;

        public MainWindow()
        {
            InitializeComponent();

            service = new RegistryService();
            backChannel = new BackChannel();

            initServer();
        }

        async Task initServer()
        {
            server = new TCPServer(54320);
            server.connectionAccepted += handleConnection;
            await server.runServerLoop();
        }

        private async void handleConnection(Object sender, TcpClient socket)
        {
            backChannel.setClient(socket);
            RequestHandler.runRequestHandlerLoop(service, backChannel, socket);
            socket.Dispose();
        }

    }
}
