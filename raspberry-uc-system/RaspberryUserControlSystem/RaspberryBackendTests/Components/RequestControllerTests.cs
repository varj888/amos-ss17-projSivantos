using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryBackend;

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


        ////Tests if unknown requests creates the corresponding exception
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void TestCreateNullCommand()
        //{
        //    Request nullRequest = new Request("nullCommand", null);
        //    requestController.handleRequest(nullRequest);

        //}


        //// Tests if possible commands create the corresponding command class
        //// Extend new commands here!
        //[TestMethod]
        //public void TestCreateCommands()
        //{
        //    Request lightLEDRequest = new Request("LightLED", 1);
        //    Command lightLEDCommand = requestController.handleRequest(lightLEDRequest);
        //    Assert.IsTrue(lightLEDCommand is LightLED);
        //    Assert.IsTrue(Command.Instances.ContainsValue(lightLEDCommand));

        //    Request readPinRequest = new Request("ReadPin", 1);
        //    Command readPinCommand = requestController.handleRequest(readPinRequest);
        //    Assert.IsTrue(readPinCommand is ReadPin);
        //    Assert.IsTrue(Command.Instances.ContainsValue(readPinCommand));

        //    Request resetPinRequest = new Request("ResetPin", 2);
        //    Command resetPinCommand = requestController.handleRequest(resetPinRequest);
        //    Assert.IsTrue(resetPinCommand is ResetPin);
        //    Assert.IsTrue(Command.Instances.ContainsValue(resetPinCommand));

        //    Request writePinRequest = new Request("WritePin", 3);
        //    Command writePinCommand = requestController.handleRequest(writePinRequest);
        //    Assert.IsTrue(writePinCommand is WritePin);
        //    Assert.IsTrue(Command.Instances.ContainsValue(writePinCommand));


        //    Request TurnOnHIRequest = new Request("TurnOnHI", 127);
        //    Command TurnOnHICommand = requestController.handleRequest(TurnOnHIRequest);
        //    Assert.IsTrue(TurnOnHICommand is SetAnalogVolume);
        //    Assert.IsTrue(Command.Instances.ContainsValue(TurnOnHICommand));

        //    Request ToggleBacklight_LCDRequest = new Request("ToggleBacklight_LCD", 1);
        //    Command ToggleBacklight_LCDCommand = requestController.handleRequest(ToggleBacklight_LCDRequest);
        //    Assert.IsTrue(ToggleBacklight_LCDCommand is ToggleBacklight_LCD);
        //    Assert.IsTrue(Command.Instances.ContainsValue(ToggleBacklight_LCDCommand));


        //    Request SendToLCDRequest = new Request("SendToLCD", "test");
        //    Command SendToLCDCommand = requestController.handleRequest(SendToLCDRequest);
        //    Assert.IsTrue(SendToLCDCommand is SendToLCD);
        //    Assert.IsTrue(Command.Instances.ContainsValue(SendToLCDCommand));

        //}

        [TestCleanup]
        public void tearDown()
        {
            requestController = null;
        }
    }
}
