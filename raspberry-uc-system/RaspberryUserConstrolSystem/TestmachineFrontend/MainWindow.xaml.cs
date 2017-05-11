using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Xml;

namespace BasicUI
{
    /// <summary>
    /// Simple UI for establishing connection
    /// to Raspberry-Pi
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //test for the class RequestConnClient
            RequestConnClient clientConnection = new RequestConnClient("minwinpc");
            clientConnection.send(new Request("lightLED", 1));
        }

        public string IPaddress { get; set; }
        public string DeviceName { get; set; }

        private void connectIP_button_Click(object sender, RoutedEventArgs e)
        {
            //TODO:add functionality here
        }

        private void vcSlider_DragStarted(object sender, RoutedEventArgs e)
        {

        }

        private void vcSlider_DragCompleted(object sender, RoutedEventArgs e)
        {

        }

        private void vcSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void soundSlider_DragStarted(object sender, RoutedEventArgs e)
        {

        }

        private void soundSlider_DragCompleted(object sender, RoutedEventArgs e)
        {

        }

        private void soundSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }

    /// <summary>
    /// This class allows to send objects of type Request to raspberryPis
    /// </summary>
    public class RequestConnClient{

        public RequestConnClient(string hostname)
        {
            connect(hostname);
        }

        private StreamWriter writer;

        /// <summary>
        /// sets up a TCP connection to the raspberry pi on port 13370
        /// </summary>
        /// <param name="hostname">
        /// name of the raspberry to connect to
        /// </param>
        private void connect(string hostname)
        {
            TcpClient client = new TcpClient(hostname, 13370);
            writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
        }

        /// <summary>
        /// 1. serializes a request into a string
        /// 2. sends the string to the raspberry Pi
        /// </summary>
        /// <param name="request"></param>
        public void send(Request request)
        {
            writer.WriteLine(Serializer.Serialize(request));
        }
}

/// <summary>
/// helper class to serialize and deserialize
/// it uses strings as the result of serialization
/// </summary>
public class Serializer
{
        /// <summary>
        /// serializes a object into a string
        /// </summary>
        /// <param name="obj">
        /// the object to serialize
        /// </param>
        /// <returns>
        /// the serialized object
        /// </returns>
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
        /// deserializes a string into a object
        /// </summary>
        /// <param name="xml">
        /// the string to deserialize
        /// </param>
        /// <param name="toType">
        /// the type of the object, that you want to get as a result by deserialization
        /// </param>
        /// <returns>
        /// the deserialized string
        /// </returns>
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

/// <summary>
/// Unit of transfer by the RequestConnClient Class
/// it is only as a container for the two variables methodName and parameter
/// note: this class uses the default contract namespace
/// </summary>
[DataContract]
public class Request
{
    public Request(string methodName, Object parameter)
    {
        this.methodName = methodName;
        this.parameter = parameter;
    }

    [DataMember]
    public string methodName;

    [DataMember]
    public Object parameter;
}
