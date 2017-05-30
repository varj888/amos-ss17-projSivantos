using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CommonFiles.Networking
{

    public class ClientConn
    {
        public static async Task<ObjConn<T>> connect<T>(string hostname, int port)
        {
            TcpClient socket = new TcpClient();
            await socket.ConnectAsync(hostname, port);
            NetworkStream stream = socket.GetStream();
            return new ObjConn<T>(stream);
        }
    }
}


