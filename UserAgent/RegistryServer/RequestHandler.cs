using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RegistryServer
{
    class RequestHandler
    {
        public static void runRequestHandlerLoop(Object callee, BackChannel backchannel, TcpClient socket)
        {
            while (true)
            {
                Request request;
                try
                {
                    request = Transfer.receiveObject<Request>(socket.GetStream());
                }
                catch(Exception e)
                {
                    Debug.WriteLine("Error receiving Object :" + e.Message);
                    return;
                }
                Debug.Write("request.command: " + request.command);
                Result result = Request.handleRequest(callee, request);
                backchannel.sendObject(result);
            }
        }
    }
}
