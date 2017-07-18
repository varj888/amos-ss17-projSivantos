using System;
using System.IO;
using System.Runtime.Serialization;

/// <summary>
/// Helper class to serialize and deserialize.
/// It uses strings as the result of serialization.
/// </summary>
namespace CommonFiles.Networking
{
    public class Serializer
    {
        /// <summary>
        /// Serializes an object into a string.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>The serialized object.</returns>
        public static string Serialize(object obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            using (StreamReader reader = new StreamReader(memoryStream))
            {
                DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
                serializer.WriteObject(memoryStream, obj);
                memoryStream.Position = 0;
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Deserializes an object from a string.
        /// </summary>
        /// <param name="xml">The string to deserialize.</param>
        /// <param name="toType">The type of the object, that you want to get as a result by deserialization.</param>
        /// <returns>The deserialized string.</returns>
        public static object Deserialize(string xml, Type toType)
        {
            using (Stream stream = new MemoryStream())
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                DataContractSerializer deserializer = new DataContractSerializer(toType);
                return deserializer.ReadObject(stream);
            }
        }
    }
}