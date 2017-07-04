using CommonFiles.TransferObjects;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace CommonFiles.Networking
{

    /// <summary>
    /// This class represents a Server which listens for incoming Requests
    /// and invokes the requested method
    /// </summary>
    public class ServerSkeleton
    {
        private Object serviceAPI;

        private int clientCount = 0;

        /// <summary>
        /// Runs the server in a new Thread
        /// </summary>
        /// <param name="serviceApi">Service, which methods will be called on incoming Request</param>
        /// <param name="port">Port, where the server listens for incoming Requests</param>
        public ServerSkeleton(Object serviceAPI, int port)
        {
            this.serviceAPI = serviceAPI;
            Task.Factory.StartNew(() => runAsync(port));
        }

        private async Task runAsync(int port)
        {
            TCPServer<Request, Result> requestServer = new TCPServer<Request, Result>(port);
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

        /// <summary>
        /// Handles the requestconnection for a client.
        /// </summary>
        /// <param name="conn"></param>
        private void handleRequestConnection(ObjConn<Request, Result> conn)
        {
            this.incClientCount();
            while (true)
            {
                //Receive a Request from the client
                Debug.WriteLine("Awaiting Request...");
                Request request = conn.receiveObject();
                Debug.WriteLine(string.Format("Received Request with content : (command= {0}) and (paramater= {1})", request.command, request.parameters));

                //Process Request
                Result result = Request.handleRequest(serviceAPI, request);

                //Send back Result to the client
                conn.sendObject(result);
            }
            this.decClientCount();
        }

        // handling the Request by searching the method request.command and calling it
        // the method will be called with request.parameter as argument
        private Result handleRequest(Request request)
        {
            MethodInfo m;

            // Searching the method
            try
            {
                m = serviceAPI.GetType().GetMethod(request.command);
            }
            catch (Exception e)
            {
                return new Result(e.Message);
            }

            if (m == null)
            {
                return new Result("Command not found");
            }

            // calling the method
            try
            {
                object value = m.Invoke(serviceAPI, request.parameters);
                return new Result(true, serviceAPI.GetType().Name, value);
            }
            catch (TargetInvocationException e)
            {
                return new Result(e.GetBaseException().Message);
            }
            catch (Exception e)
            {
                return new Result(e.Message);
            }
        }

        /// <summary>
        /// Increase the client-counter synchronously to avoid race-conditions.
        /// </summary>
        private void incClientCount()
        {
            lock (this)
            {
                clientCount++;
            }
        }

        /// <summary>
        /// Decrease the client-counter synchronously to avoid race-conditions.
        /// </summary>
        private void decClientCount()
        {
            lock (this)
            {
                clientCount--;
            }
        }

        /// <summary>
        /// Retrieve the client-counter synchronously to avoid race-conditions.
        /// </summary>
        /// <returns>Client counter</returns>
        public int getClientCount()
        {
            lock (this)
            {
                return this.clientCount;
            }
        }
    }
}
