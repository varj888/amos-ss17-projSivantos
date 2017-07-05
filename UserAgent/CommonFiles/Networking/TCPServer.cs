using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CommonFiles.Networking
{

    /// <summary>
    /// Allows to Listen for Connections and accepting them
    /// </summary>
    public class TCPServer: IDisposable
    {
        private TcpListener listener;

        public EventHandler<TcpClient> connectionAccepted;

        /// <summary>
        /// creates a Server wich will listen on a port for TCP Connections
        /// </summary>
        /// <param name="port">Port, where TCPServer listens for incomming Connections</param>
        public TCPServer(int port)
        {
            // Set the TcpListener on Port port
            listener = new TcpListener(IPAddress.Any, port);

            // Start listening for client requests
            listener.Start();
        }

        public async Task runServerLoop()
        {
            while (true)
            {
                TcpClient socket;
                try
                {
                    socket = await listener.AcceptTcpClientAsync();
                }catch(Exception e)
                {
                    Debug.WriteLine("Error Accepting Connection: " + e.Message);
                    continue;
                }
                onConnectionAccepted(socket);
            }
        }

        // Disposes the server
        public void Dispose()
        {
            listener.Stop();
        }

        private void onConnectionAccepted(TcpClient socket)
        {
            EventHandler<TcpClient> handler = connectionAccepted;
            if (connectionAccepted != null)
            {
                handler(this, socket);
            }
        }
    }


}
