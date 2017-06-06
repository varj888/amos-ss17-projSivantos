
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryBackend;
using CommonFiles.TransferObjects;

namespace RaspberryBackendTests
{
    [TestClass]
    public class RaspberryPiTests
    {
        RaspberryPi raspberryPi;
        GPIOinterface TestGpiooInterface;
        LCD TestLcdDisplay;
        Potentiometer Testpotentiometer;


        [TestInitialize]
        public void setUp()
        {
            raspberryPi = RaspberryPi.Instance;
            TestGpiooInterface = new GPIOinterface();
            TestLcdDisplay = new LCD();
            Testpotentiometer = new Potentiometer();
        }

        [TestMethod]
        public void TestNotInitialized()
        {
            raspberryPi.reset();
            Assert.IsNull(raspberryPi.GpioInterface);
            Assert.IsNull(raspberryPi.LcdDisplay);
            Assert.IsNull(raspberryPi.Potentiometer);
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
