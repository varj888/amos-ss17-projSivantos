using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryBackend.Components
{
    /// <summary>
    /// This class can be used to remotely call Methods of a client
    /// </summary>
    class ServerStub: IDisposable
    {
        private ObjConn<Result, Request> connection;

        /// <summary>
        /// Runs a server, wich waits for a client to connect
        /// </summary>
        /// <param name="port">Port, where the server listens for a connection</param>
        /// <returns>The created Skeleton</returns>
        public static async Task<ServerStub> createServerStubAsync(int port)
        {
            using (TCPServer<Result, Request> requestServer = new TCPServer<Result, Request>(port))
            {
                ObjConn<Result, Request> connection = await requestServer.acceptConnectionAsync();
                return new ServerStub(connection);
            }
        }

        private ServerStub(ObjConn<Result, Request> connection)
        {
            this.connection = connection;
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        /// <summary>
        /// Sends a Request to call a Method with the same signature as this Method at the client
        /// </summary>
        /// <param name="parameter"></param>
        public void testCall(string parameter)
        {
            connection.sendObject(new Request("testCall", parameter));
        }
    }
}
