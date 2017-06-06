
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryBackend;
using CommonFiles.TransferObjects;
using CommonFiles.Networking;
using System.Threading.Tasks;
using System.Net;

namespace RaspberryBackendTests
{
    [TestClass]
    public class ClientServerTests
    {

        private const int _PORT = 12000;
        private const string _LOCALHOST = "127.0.0.1";

        private const string _TESTCOMMAND_1 = "TestCommand_1";
        private const int _TESTPARAM_1 = 1;

        private const string _TESTCOMMAND_2 = "TestCommand_2";
        private const int _TESTPARAM_2 = 2;

        TCPServer<Request, Result> tcpServer;
        ClientConn<Result, Request> tcpClient;

        ObjConn<Request, Result> connection;

        [TestInitialize]
        public async Task setUp()
        {
            tcpServer = new TCPServer<Request, Result>(_PORT);
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(_LOCALHOST), _PORT);
            tcpClient = await ClientConn<Result,Request>.connectAsync(endpoint);

            connection = await tcpServer.acceptConnectionAsync();
        }

        [TestMethod]
        public void TestSendReceiveObjects()
        {
            tcpClient.sendObject(new Request(_TESTCOMMAND_1, _TESTPARAM_1));
            tcpClient.sendObject(new Request(_TESTCOMMAND_2, _TESTPARAM_2));

            Request request_1 = connection.receiveObject();

            Assert.AreEqual(request_1.command, _TESTCOMMAND_1);
            Assert.AreEqual(request_1.parameter, _TESTPARAM_1);

            Request request_2 = connection.receiveObject();

            Assert.AreEqual(request_2.command, _TESTCOMMAND_2);
            Assert.AreEqual(request_2.parameter, _TESTPARAM_2);
        }
    }
}
