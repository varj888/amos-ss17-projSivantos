using System.IO;
using System.Net.Sockets;

namespace TestmachineFrontend
{

    /// <summary>
    /// Represents a connection to a server, which allows to send and receive Objects of Type T
    /// The Server has to use the same serialisation methods used in this implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjConnClient<T>
    {
        private StreamReader reader;
        private StreamWriter writer;

        /// <summary>
        /// Connects to the server over TCP
        /// </summary>
        /// <param name="hostname">
        /// hostname of the server
        /// </param>
        /// <param name="port">
        /// port of the server
        /// </param>
        public ObjConnClient(string hostname, int port)
        {
            TcpClient client = new TcpClient(hostname, port);
            NetworkStream stream = client.GetStream();
            writer = new StreamWriter(stream) { AutoFlush = true };
            reader = new StreamReader(stream);
        }

        /// <summary>
        /// 1. Serializes a Object of Type T into a String
        /// 2. Sends the string to the connected Server
        /// </summary>
        /// <param name="obj"></param>
        public void sendObject(T obj)
        {
            writer.WriteLine(Serializer.Serialize(obj));
        }

        /// <summary>
        /// Receives a String from a Server and Deserializes it into an Object of Type T
        /// It is a blocking Operation
        /// </summary>
        /// <returns></returns>
        public T receiveObject()
        {
            return (T)Serializer.Deserialize(reader.ReadLine(), typeof(T));
        }

    }
}
