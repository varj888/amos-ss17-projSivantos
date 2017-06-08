using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryBackend;

namespace RaspberryBackendTests
{
    [TestClass]
    public class RaspberryPiTests
    {
        RaspberryPi raspberryPi;
        GPIOinterface TestGpiooInterface;
        LCD TestLcdDisplay;
        Potentiometer Testpotentiometer;
        Multiplexer Testmultiplexer;
        ADCDAC Testadconverter;

        [TestInitialize]
        public void setUp()
        {
            raspberryPi = RaspberryPi.Instance;
            TestGpiooInterface = new GPIOinterface();
            TestLcdDisplay = new LCD();
            Testpotentiometer = new Potentiometer();
            Testmultiplexer = new Multiplexer();
            Testadconverter = new ADCDAC();
        }

        [TestMethod]
        public void TestNotInitialized()
        {
            raspberryPi.reset();
            System.Diagnostics.Debug.WriteLine(raspberryPi.GPIOinterface != null ? raspberryPi.GPIOinterface.ToString() : "null");
            Assert.IsNull(raspberryPi.GPIOinterface);
            Assert.IsNull(raspberryPi.LCD);
            Assert.IsNull(raspberryPi.Potentiometer);
        }

        [TestMethod]
        public void TestIsInitialized()
        {
            raspberryPi.reset();
            raspberryPi.initialize(TestGpiooInterface, TestLcdDisplay, Testpotentiometer, Testmultiplexer, Testadconverter);

            Assert.AreEqual(TestGpiooInterface, raspberryPi.GPIOinterface);
            Assert.AreEqual(TestLcdDisplay, raspberryPi.LCD);
            Assert.AreEqual(Testpotentiometer, raspberryPi.Potentiometer);
            Assert.AreEqual(Testmultiplexer, raspberryPi.Multiplexer);
            Assert.AreEqual(Testadconverter, raspberryPi.ADCDAC);
        }

        [TestCleanup]
        public void tearDown()
        {
            raspberryPi.reset();
            raspberryPi = null;
            TestGpiooInterface = null;
            TestLcdDisplay = null;
            Testpotentiometer = null;
        }
    }
}
