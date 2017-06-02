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
    /// Represents a connection which allows to send and receive Byte Arrays
    /// </summary>
    class DataConn: IDisposable
    {
        private NetworkStream stream;

        /// <summary>
        /// Creates the DataConn from a stream
        /// </summary>
        /// <param name="stream">stream which will be used for sending and receiving Byte Arrays</param>
        public DataConn(NetworkStream stream)
        {
            this.stream = stream;
        }

        /// <summary>
        /// sends a byte array over the stream
        /// </summary>
        /// <param name="data">Byte Array, which will be send over the stream</param>
        public void send(byte[] data)
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
        /// receives a byte Array from the stream
        /// </summary>
        /// <returns> Byte Array received from the stream</returns>
        public byte[] receive()
        {
            byte[] payloadSize = read(stream, 4);
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(payloadSize);
            }
            int count = BitConverter.ToInt32(payloadSize, 0);
            return read(stream, count);
        }

        public void Dispose()
        {
            stream.Dispose();
        }

        private byte[] read(NetworkStream stream, int count)
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
    }
}
