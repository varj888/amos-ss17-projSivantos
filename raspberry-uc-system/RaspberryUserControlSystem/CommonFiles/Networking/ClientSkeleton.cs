using CommonFiles.TransferObjects;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CommonFiles.Networking
{
    /// <summary>
    /// This class can be used as RPC proxy. It allows to remotely call Methods of a local Object.
    /// </summary>
    public class ClientSkeleton : IDisposable
    {
        ClientConn<Request, Result> conn;

        /// <summary>
        /// Creates the Skeleton by connecting to the server which will send Requests
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static async Task<ClientSkeleton> createClientSkeletonAsync(IPEndPoint endpoint)
        {
            ClientConn<Request,Result> conn = await ClientConn<Request, Result>.connectAsync(endpoint);
            return new ClientSkeleton(conn);
        }

        /// <summary>
        /// Invokes the requested Methods and sends back the results
        /// </summary>
        /// <param name="callee">Object, which Methods will be called</param>
        public void runRequestLoop(Object callee)
        {
            while (true)
            {
                Request request = conn.receiveObject();
                Result result = Others.handleRequest(callee, request);
                conn.sendObject(result);
            }
        }

        private ClientSkeleton(ClientConn<Request, Result> conn)
        {
            this.conn = conn;
        }

        /// <summary>
        /// Disposes the Skeleton by closing the Connection to the Server
        /// </summary>
        public void Dispose()
        {
            conn.Dispose();
        }
    }
}
