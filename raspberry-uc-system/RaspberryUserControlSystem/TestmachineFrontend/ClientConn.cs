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
    /// Represents a TCP connection to a Server
    /// </summary>
    /// <typeparam name="T">Type of the Object, which will be transfered between client and server</typeparam>
    public class ClientConn<T>
    {
       private ObjConn<T> conn;
    
        /// <summary>
        /// Creates a connection to a server
        /// </summary>
        /// <param name="hostname">hostname of the server to connect to</param>
        /// <param name="port">port of the server, to connect to</param>
       public ClientConn(string hostname, int port)
        {
            TcpClient client = new TcpClient(hostname, port);
            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
            StreamReader reader = new StreamReader(stream);
            conn = new ObjConn<T>(reader, writer);
        }
        
        /// <summary>
        /// Sends an Object of Type T to the connected Server
        /// </summary>
        /// <param name="obj">object which will be send to the server</param>
        public void sendObject(T obj)
        {
            conn.sendObject(obj);
        }

        /// <summary>
        /// Receives an Object of Type T from the server
        /// It is a blocking Operation
        /// </summary>
        /// <returns>The Object of Type T received from the server</returns>
        public T receiveObject()
        {
            return conn.receiveObject();
        }

    }
}


