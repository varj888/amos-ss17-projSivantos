using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestmachineFrontend
{
    /// <summary>
    /// This class allows to send objects of type Request to raspberryPis
    /// </summary>
    public class RequestConnClient
    {
        ObjConnClient<Request> conn;

        /// <summary>
        /// connects to the raspberry pi on port 13370
        /// </summary>
        /// <param name="hostname">
        /// hostname or IP-Address of the raspberry pi to connect to
        /// </param>
        public RequestConnClient(string hostname)
        {
            conn = new ObjConnClient<Request>(hostname, 13370);
        }


        /// <summary>
        /// 1. serializes a request into a string
        /// 2. sends the string to the raspberry Pi
        /// </summary>
        /// <param name="request"></param>
        public void send(Request request)
        {
            conn.sendObject(request);
        }
    }
}
