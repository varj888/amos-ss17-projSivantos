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

        public RequestConnClient(string hostname)
        {
            connect(hostname);
        }

        private StreamWriter writer;

        /// <summary>
        /// sets up a TCP connection to the raspberry pi on port 13370
        /// </summary>
        /// <param name="hostname">
        /// name of the raspberry to connect to
        /// </param>
        private void connect(string hostname)
        {
            TcpClient client = new TcpClient(hostname, 13370);
            writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
        }

        /// <summary>
        /// 1. serializes a request into a string
        /// 2. sends the string to the raspberry Pi
        /// </summary>
        /// <param name="request"></param>
        public void send(Request request)
        {
            writer.WriteLine(Serializer.Serialize(request));
        }
    }
}
