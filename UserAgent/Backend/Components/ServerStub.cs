using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using CommonFiles.RPCInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace RaspberryBackend.Components
{
    /// <summary>
    /// This class can be used to remotely call Methods of a client
    /// </summary>
    class ServerStub: IDisposable, IEventReceiver
    {
        private TcpClient socket;

        /// <summary>
        /// Runs a server, wich waits for a client to connect
        /// </summary>
        /// <param name="port">Port, where the server listens for a connection</param>
        /// <returns>The created Skeleton</returns>
        public static async Task<ServerStub> createServerStubAsync(int port)
        {
            using (TCPServer requestServer = new TCPServer(port))
            {
                TcpClient socket = await requestServer.acceptConnectionAsync();
                return new ServerStub(socket);
            }
        }

        private ServerStub(TcpClient socket)
        {
            this.socket = socket;
        }

        /// <summary>
        /// Disposes the ServerStub
        /// </summary>
        public void Dispose()
        {
            socket.Dispose();
        }

        /// <summary>
        /// Sends a Request to call a Method with the same signature as this Method at the client
        /// </summary>
        /// <param name="parameter"></param>
        public void testCall(string parameter)
        {
            Transfer.sendObject(socket.GetStream(), new Request("testCall", parameter));
        }
    }
}
