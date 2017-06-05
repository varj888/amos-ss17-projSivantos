using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestmachineFrontend
{
    /// <summary>
    /// Software representation of the RaspberryPi. It contains all component representations which are phyisical connected to the Rpi. 
    /// </summary>
    public sealed class RaspberryPi
    {
        private ClientConn<Result, Request> clientConnection;
        public IPEndPoint endpoint { get; private set; }
        private static UInt16 counter;
        public String name { get; set; }
        public int ID { get; private set; }

        /// <summary>
        ///     Event occurs when connection is made.
        /// </summary>
        public event EventHandler Connected;

        /// <summary>
        ///     Event triggers when a connection is established.
        /// </summary>
        public bool IsConnected { get; private set; }

        public static async Task<RaspberryPi> Create(IPEndPoint endpoint)
        {
            var raspberryPi = new RaspberryPi(endpoint);
            await raspberryPi.Initialize();
            return raspberryPi;
        }
        // Private constructor because due to its async nature of networking actual initialization can't occur here.
        // Instead a pseudo-factory is employed that returns an initialized, connected RaspberryPi Object to the Caller,
        // because the instance relies on an already established connection.
        private RaspberryPi(IPEndPoint endpoint)
        {
            this.endpoint = endpoint;
            counter++; //currently unused
            ID = counter;
        }
        // Does necessary asynchronous work to initialize / connect RaspberryPi.
        // Note that this function might throw an exception when connection can't be established.
        private async Task Initialize()
        {
            try
            {
                Console.WriteLine("[x] Connecting to: " + this.endpoint.Address + " ...");
                clientConnection = await ClientConn<Result, Request>.connectAsync(this.endpoint); // port usually 54321
                /* Connection established, set IsConnected to true because async call returned. */
                IsConnected = true; 
                /* Fire the Connected event handler. Unused currently. */
                Connected?.Invoke(this, EventArgs.Empty);
            }
            /* If host is offline, display the socket exception and stop running */
            catch (SocketException sx)
            {
                IsConnected = false;
                Console.WriteLine("[ERROR] Connection couldn't be established. Host offline. ", sx);
                throw new SocketException(sx.ErrorCode);
            }
            /* If initialization fails, display the exception and stop running */
            catch (Exception ex)
            {
                IsConnected = false;
                Console.WriteLine("[ERROR] Connection couldn't be established for unknown reasons.");
                throw new Exception("Connection couldn't be established for unknown reasons.", ex);
            }
           
        }

        public void readPin(UInt16 PinID)
        {

            try
            {
                clientConnection.sendObject(new Request("ReadPin", PinID));
                return;

            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] Request could not be sent: " + ex.Message);
                throw new Exception("Request could not be sent:", ex);
            }
        }

        public void writePin(UInt16 PinID)
        {

            try
            {
                clientConnection.sendObject(new Request("WritePin", PinID));
                return;

            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] Request could not be sent: " + ex.Message);
                throw new Exception("Request could not be sent:", ex);
            }
        }

        public void resetPin(UInt16 PinID)
        {

            try
            {
                clientConnection.sendObject(new Request("ResetPin", PinID));
                return;

            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] Request could not be sent: " + ex.Message);
                throw new Exception("Request could not be sent:", ex);
            }
        }

        public void lightLED(Boolean light_on)
        {
            try
            {
                if (light_on)
                {
                    clientConnection.sendObject(new Request("LightLED", 1));
                }
                else
                {
                    clientConnection.sendObject(new Request("LightLED", 0));
                }
                return;

            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] Request could not be sent: " + ex.Message);
                throw new Exception("Request could not be sent:", ex);
            }
        }

    }
}
