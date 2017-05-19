using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

namespace RaspberryBackend
{
    public delegate void ConnectionHandler<T>(ObjConn<T> conn);

    /// <summary>
    /// Allows to Listen for Connections and handle them
    /// </summary>
    /// <typeparam name="T">Type of the Object, wich will be transfered between client and server</typeparam>
    public class TCPServer<T>
    {
        private ConnectionHandler<T> handle;

        /// <summary>
        /// creates a Server wich will listen on a port for TCP Connections
        /// incomming Connections will be handled by handler
        /// </summary>
        /// <param name="port">Port, where TCPServer listens for incomming Connections</param>
        /// <param name="handler">handler, which will be called for every incomming Connection</param>
        public TCPServer(int port, ConnectionHandler<T> handler)
        {
            this.handle = handler;
            createListenerAsync(port);
        }

        private async Task createListenerAsync(int port)
        {
            //Create a StreamSocketListener to start listening for TCP connections.
            StreamSocketListener socketListener = new StreamSocketListener();

            //Hook up an event handler to call when connections are received.
            socketListener.ConnectionReceived += SocketListener_ConnectionReceived;
            Debug.WriteLine("create Listener");

            //Start listening for incoming TCP connections on the specified port
            await socketListener.BindServiceNameAsync(port.ToString());
            Debug.WriteLine("created Listener");
        }

        private async void SocketListener_ConnectionReceived(StreamSocketListener sender,
           StreamSocketListenerConnectionReceivedEventArgs args)
        {
            StreamReader reader = new StreamReader(args.Socket.InputStream.AsStreamForRead());
            StreamWriter writer = new StreamWriter(args.Socket.OutputStream.AsStreamForWrite()) { AutoFlush = true };

            handle(new ObjConn<T>(reader, writer));
        }
    }


}
