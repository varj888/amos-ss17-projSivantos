using System;
using System.Net.Sockets;
using System.Text;

namespace CommonFiles.Networking
{
    /// <summary>
    /// Represents a TCP connection, which allows to send and receive Objects
    /// </summary>
    /// <typeparam name="inType"> Type of the Objects, which will be received over ObjConn</typeparam>
    /// <typeparam name="outType"> Type of the Objects, which will be send over ObjConn </typeparam>
    public class ObjConn<inType, outType> : IDisposable
    {
        DataConn dataConn;

        /// <summary>
        /// creates the ObjConn from a stream. The stream should already be connected to the communication-partner
        /// </summary>
        /// <param name="stream">stream, the ObjConn will use for sending and receiving Objects</param>
        public ObjConn(NetworkStream stream)
        {
            dataConn = new DataConn(stream);
        }

        /// <summary>
        /// Sends an Object of Type outType over the connection
        /// </summary>
        /// <param name="obj">object to send over the stream</param>
        public void sendObject(outType obj)
        {
            dataConn.send(Encoding.ASCII.GetBytes(Serializer.Serialize(obj)));
        }

        /// <summary>
        /// receives an Object from Type inType from the connection
        /// It is a blocking operation
        /// </summary>
        /// <returns>Object received from the stream</returns>
        public inType receiveObject()
        {
            return (inType)Serializer.Deserialize(Encoding.ASCII.GetString(dataConn.receive()), typeof(inType));
        }

        public void Dispose()
        {
            dataConn.Dispose();
        }
    }
}