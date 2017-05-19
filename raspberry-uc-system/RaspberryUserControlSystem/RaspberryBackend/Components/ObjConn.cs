using System.IO;

namespace RaspberryBackend
{
    /// <summary>
    /// Represents a connection to a server, which allows to send and receive Objects of Type T
    /// The Server has to use the same serialisation methods used in this implementation
    /// </summary>
    /// <typeparam name="T">Type of The Object, wich will be send over the connection</typeparam>
    public class ObjConn<T>
    {
        StreamReader reader;
        StreamWriter writer;

        /// <summary>
        /// Is initialised by streams which should already be initialised
        /// </summary>
        /// <param name="inStream">stream, the ObjConn will use for receiving Objects</param>
        /// <param name="outStream">stream, the ObjConn will use for sending Objects</param>
        public ObjConn(StreamReader inStream, StreamWriter outStream)
        {
            reader = inStream;
            writer = outStream;
        }

        /// <summary>
        /// Serialises an object of type T into a String and writes the string on the outStream
        /// </summary>
        /// <param name="obj">object to send over the stream</param>
        public void sendObject(T obj)
        {
            writer.WriteLine(Serializer.Serialize(obj));
        }

        /// <summary>
        /// receives a String from the Stream and deserializes it into an Object of Type T
        /// It is a blocking operation
        /// </summary>
        /// <returns>Object received from the stream</returns>
        public T receiveObject()
        {
            return (T)Serializer.Deserialize(reader.ReadLine(), typeof(T));
        }
    }
}