
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


        //Tests if unknown requests creates the corresponding exception
        [TestMethod]
        public void TestNotInitialized()
        {
            raspberryPi.reset();
            Assert.IsNull(raspberryPi.GpioInterface);
            Assert.IsNull(raspberryPi.LcdDisplay);
            Assert.IsNull(raspberryPi.Potentiometer);
        }


        // Tests if possible commands create the corresponding command class
        // Extend new commands here!
        [TestMethod]
        public void TestSingleInitialize()
        {
            raspberryPi.initialize();
            raspberryPi.initialize(TestGpiooInterface, TestLcdDisplay, Testpotentiometer);
            Assert.AreNotEqual(raspberryPi.GpioInterface, TestGpiooInterface);
            Assert.AreNotEqual(raspberryPi.LcdDisplay, TestLcdDisplay);
            Assert.AreNotEqual(raspberryPi.Potentiometer, Testpotentiometer);
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
