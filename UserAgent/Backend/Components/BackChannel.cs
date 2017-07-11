using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryBackend.Components
{
    /// <summary>
    /// Socket, which can be used by multiple threads for sending
    /// </summary>
    public class BackChannel
    {
        private TcpClient socket;
       
        /// <summary>
        /// Sets the socket
        /// </summary>
        /// <param name="socket">Socket, which will be used for sending</param>
        public void setClient(TcpClient socket)
        {
            this.socket = socket;
        }

        /// <summary>
        /// Sends an Objecto over the socket
        /// </summary>
        /// <param name="obj">Object which will be send</param>
        public void sendObject(Object obj)
        {
            lock (this)
            {
                Transfer.sendObject(socket.GetStream(), obj);
            }
        }
    }
}
