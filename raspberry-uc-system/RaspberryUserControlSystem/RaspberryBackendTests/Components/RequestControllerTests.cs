
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryBackend;
using CommonFiles.TransferObjects;

namespace RaspberryBackendTests
{
    [TestClass]
    public class RequestControllerTests
    {
        RequestController requestController;
        GPIOinterface inter;

        [TestInitialize]
        public void setUp()
        {
            inter = new GPIOinterface();
            //do not init the pins
            requestController = RequestController.Instance;

        }


        //Tests if unknown requests creates the corresponding exception
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateNullCommand()
        {
            Request nullRequest = new Request("nullCommand", null);
            requestController.handleRequest(nullRequest);
        }


        // Tests if possible commands create the corresponding command class
        // Extend new commands here!
        [TestMethod]
        public void TestCreateCommands()
        {
            Request lightLEDRequest = new Request("LightLED", 1);
            Command lightLEDCommand = requestController.handleRequest(lightLEDRequest);
            Assert.IsTrue(lightLEDCommand is LightLED);
         
            Request readPinRequest = new Request("ReadPin", 1);
            Command readPinCommand = requestController.handleRequest(readPinRequest);
            Assert.IsTrue(readPinCommand is ReadPin);

            Request resetPinRequest = new Request("ResetPin", 2);
            Command resetPinCommand = requestController.handleRequest(resetPinRequest);
            Assert.IsTrue(resetPinCommand is ResetPin);

            Request writePinRequest = new Request("WritePin", 3);
            Command writePinCommand = requestController.handleRequest(writePinRequest);
            Assert.IsTrue(writePinCommand is WritePin);
        }

        [TestCleanup]
        public void tearDown()
        {
            requestController = null;
        }
    }
}
