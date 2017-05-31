using System;
using System.IO;
using System.Net.Sockets;

namespace CommonFiles.Networking
{
    /// <summary>
    /// Represents a connection to a server, which allows to send and receive Objects
    /// The Server has to use the same serialisation methods used in this implementation
    /// </summary>
    /// <typeparam name="inType"> Type of the Objects, which will be received over ObjConn</typeparam>
    /// <typeparam name="outType"> Type of the Objects, which will be send over ObjConn </typeparam>
    public class ObjConn<inType, outType> : IDisposable
    {
        NetworkStream stream;
        StreamReader reader;
        StreamWriter writer;

        /// <summary>
        /// Is initialised by streams which should already be initialised
        /// </summary>
        /// <param name="stream">stream, the ObjConn will use for sending and receiving Objects</param>
        public ObjConn(NetworkStream stream)
        {
            this.stream = stream;
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };
        }

        /// <summary>
        /// Serialises an object of type outType into a String and writes the string on the outStream
        /// </summary>
        /// <param name="obj">object to send over the stream</param>
        public void sendObject(outType obj)
        {
            writer.WriteLine(Serializer.Serialize(obj));
        }

        /// <summary>
        /// receives a String from the Stream and deserializes it into an Object of Type inType
        /// It is a blocking operation
        /// </summary>
        /// <returns>Object received from the stream</returns>
        public inType receiveObject()
        {
            return (inType)Serializer.Deserialize(reader.ReadLine(), typeof(inType));
        }

        public void Dispose()
        {
            stream.Dispose();
        }
    }
}