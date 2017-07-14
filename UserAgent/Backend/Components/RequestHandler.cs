using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryBackend.Components
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
                    request = (Request)Transfer.receiveObject(socket.GetStream());
                }
                catch(Exception e)
                {
                    Debug.WriteLine("Error receiving Object :" + e.Message);
                    return;
                }
                Object reply = Request.handleRequest(callee, request);
                backchannel.sendObject(reply);
            }
        }
    }
}
