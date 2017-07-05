using CommonFiles.TransferObjects;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

//namespace CommonFiles.Networking
//{

//    /// <summary>
//    /// This class represents a Server which listens for incoming Requests
//    /// and invokes the requested method
//    /// </summary>
//    public class ServerSkeleton
//    {
//        private Object service;
//        private int clientCount = 0;

//        /// <summary>
//        /// Runs the server in a new Thread
//        /// </summary>
//        /// <param name="service">Service, which methods will be called on incoming Request</param>
//        /// <param name="port">Port, where the server listens for incoming Requests</param>
//        public ServerSkeleton(Object service, int port)
//        {
//            this.service = service;
//            Task.Factory.StartNew(() => runAsync(port));
//        }

//        private async Task runAsync(int port)
//        {
//            TCPServer requestServer = new TCPServer(port);
//            while (true)
//            {
//                try
//                {
//                    Debug.WriteLine(this.GetType().Name + "::: Awaiting request...");
//                    using (TcpClient socket = await requestServer.acceptConnectionAsync())
//                    {
//                        handleRequestConnection(socket);
//                    }
//                }
//                catch (Exception e)
//                {
//                    Debug.WriteLine("Network error: " + e.Message);
//                }
//            }
//        }

//    /// <summary>
//    /// Handles the requestconnection for a client.
//    /// </summary>
//    /// <param name="conn"></param>
//    private void handleRequestConnection(TcpClient socket)
//        {
//            this.incClientCount();
//            while (true)
//            {
//                //Receive a Request from the client
//                Debug.WriteLine("Awaiting Request...");
//                Request request = Transfer.receiveObject<Request>(socket.GetStream());
//                Debug.WriteLine(string.Format("Received Request with content : (command= {0}) and (paramater= {1})", request.command, request.parameters));

//                //Process Request
//                Result result = Request.handleRequest(service, request);

//                //Send back Result to the client
//                Transfer.sendObject(socket.GetStream(), result);
//            }
//            this.decClientCount();
//        }

//        // handling the Request by searching the method request.command and calling it
//        // the method will be called with request.parameter as argument
//        private Result handleRequest(Request request)
//        {
//            MethodInfo m;

//            // Searching the method
//            try
//            {
//                m = service.GetType().GetMethod(request.command);
//            }
//            catch (Exception e)
//            {
//                return new Result(e.Message);
//            }

//            if (m == null)
//            {
//                return new Result("Command not found");
//            }

//            // calling the method
//            try
//            {
//                object value = m.Invoke(service, request.parameters);
//                return new Result(true,service.GetType().Name, value);
//            }
//            catch (TargetInvocationException e)
//            {
//                return new Result(e.GetBaseException().Message);
//            }
//            catch (Exception e)
//            {
//                return new Result(e.Message);
//            }
//        }

//        /// <summary>
//        /// Increase the client-counter synchronously to avoid race-conditions.
//        /// </summary>
//        private void incClientCount()
//        {
//            lock (this)
//            {
//                clientCount++;
//            }
//        }

//        /// <summary>
//        /// Decrease the client-counter synchronously to avoid race-conditions.
//        /// </summary>
//        private void decClientCount()
//        {
//            lock (this)
//            {
//                clientCount--;
//            }
//        }

//        /// <summary>
//        /// Retrieve the client-counter synchronously to avoid race-conditions.
//        /// </summary>
//        /// <returns>Client counter</returns>
//        public int getClientCount()
//        {
//            lock (this)
//            {
//                return this.clientCount;
//            }
//        }
//    }
//}
