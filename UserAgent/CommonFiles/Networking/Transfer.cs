using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CommonFiles.Networking
{
    /// <summary>
    /// Contains Methods to send byte-Arrays and objects over a Networkstream.
    /// </summary>
    class Transfer
    {
      
        /// <summary>
        /// Sends a byte array over a Networkstream.
        /// </summary>
        /// <param name="stream">Networkstream used for sending.</param>
        /// <param name="data">Byte Array, which will be send.</param>
        public static void sendByteArray(NetworkStream stream, byte[] data)
        {
            byte[] payloadSize = BitConverter.GetBytes(data.Length);
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(payloadSize);
            }
            stream.Write(payloadSize, 0, payloadSize.Length);
            stream.Write(data, 0, data.Length);
            stream.Flush();
        }

        /// <summary>
        /// Sends a byte array asynchronously over a networkstream.
        /// </summary>
        /// <param name="stream">Networkstream used for sending.</param>
        /// <param name="data">Byte array to send</param>
        public static async Task sendByteArrayAsync(NetworkStream stream, byte[] data)
        {
            byte[] payloadSize = BitConverter.GetBytes(data.Length);
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(payloadSize);
            }
            await stream.WriteAsync(payloadSize, 0, payloadSize.Length);
            await stream.WriteAsync(data, 0, data.Length);
            await stream.FlushAsync();
        }

        /// <summary>
        /// Receives a byte Array from the stream.
        /// </summary>
        /// <param name="stream">Networkstream used for sending.</param>
        /// <returns>Byte array received from the stream.</returns>
        public static byte[] receiveByteArray(NetworkStream stream)
        {
            byte[] payloadSize = read(stream, 4);
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(payloadSize);
            }
            int count = BitConverter.ToInt32(payloadSize, 0);
            return read(stream, count);
        }

        /// <summary>
        /// Receives a byte array asynchronously from the stream.
        /// </summary>
        /// <param name="stream">Networkstream used for sending.</param>
        /// <returns>Byte array received from the stream.</returns>
        public static async Task<byte[]> receiveByteArrayAsync(NetworkStream stream)
        {
            byte[] payloadSize = await readAsync(stream, 4);
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(payloadSize);
            }
            int count = BitConverter.ToInt32(payloadSize, 0);
            return await readAsync(stream, count);
        }

        /// <summary>
        /// Read a number of bytes from a networkstream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The bytes read.</returns>
        private static byte[] read(NetworkStream stream, int count)
        {
            byte[] result = new byte[count];
            int offset = 0;
            int read = 0;
            while (count > 0 && (read = stream.Read(result, offset, count)) > 0)
            {
                offset += read;
                count -= read;
            }
            if (count != 0)
            {
                throw new EndOfStreamException();
            }
            return result;
        }

        /// <summary>
        /// Read a number of bytes asynchronously from a networkstream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The bytes read.</returns>
        private static async Task<byte[]> readAsync(NetworkStream stream, int count)
        {
            byte[] result = new byte[count];
            int offset = 0;
            int read = 0;
            while (count > 0 && (read = await stream.ReadAsync(result, offset, count)) > 0)
            {
                offset += read;
                count -= read;
            }
            if (count != 0)
            {
                throw new EndOfStreamException();
            }
            return result;
        }

        /// <summary>
        /// Sends an object over a networkstream.
        /// </summary>
        /// <param name="stream">Stream used for sending.</param>
        /// <param name="obj">Object, which will be sent.</param>
        public static void sendObject(NetworkStream stream, Object obj)
        {
            sendByteArray(stream, Encoding.ASCII.GetBytes(obj.GetType().FullName));
            sendByteArray(stream, Encoding.ASCII.GetBytes(Serializer.Serialize(obj)));
        }

        /// <summary>
        /// Sends an Object asynchronously over a Networkstream.
        /// </summary>
        /// <param name="stream">Stream used for sending.</param>
        /// <param name="obj">Object, which will be sent.</param>
        public static async Task sendObjectAsync(NetworkStream stream, Object obj)
        {
            await sendByteArrayAsync(stream, Encoding.ASCII.GetBytes(obj.GetType().FullName));
            await sendByteArrayAsync(stream, Encoding.ASCII.GetBytes(Serializer.Serialize(obj)));
        }

        /// <summary>
        /// Receives an object from a stream.
        /// </summary>
        /// <param name="stream">Stream used for sending.</param>
        /// <returns>Object received from the stream.</returns>
        public static Object receiveObject(NetworkStream stream)
        {
            Type t = Type.GetType(Encoding.ASCII.GetString(receiveByteArray(stream)));
            return Serializer.Deserialize(Encoding.ASCII.GetString(receiveByteArray(stream)), t);
        }

        /// <summary>
        /// Asynchronously receives an object from a stream.
        /// </summary>
        /// <param name="stream">Stream used for sending.</param>
        /// <returns>Object received from the stream.</returns>
        public static async Task<Object> receiveObjectAsync(NetworkStream stream)
        {
            Type t = Type.GetType(Encoding.ASCII.GetString(await receiveByteArrayAsync(stream)));
            return Serializer.Deserialize(Encoding.ASCII.GetString(await receiveByteArrayAsync(stream)), t);
        }
    }
}
