using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryBackend;
using System;

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
        ADConverter Testadconverter;

        HWComponent[] testAllComponents;
        HWComponent[] testPartComponents;
        HWComponent[] testDuplicateComponents;

        [TestInitialize]
        public void setUp()
        {
            raspberryPi = RaspberryPi.Instance;
            testAllComponents = new HWComponent[] { new GPIOinterface(), new LCD(), new Potentiometer(), new Multiplexer(), new ADConverter() };
            testPartComponents = new HWComponent[] { new GPIOinterface(), new Multiplexer(), new ADConverter() };
            testDuplicateComponents = new HWComponent[] { new GPIOinterface(), new Multiplexer(), new Multiplexer(), new ADConverter() };

            TestGpiooInterface = new GPIOinterface();
            TestLcdDisplay = new LCD();
            Testpotentiometer = new Potentiometer();
            Testmultiplexer = new Multiplexer();
            Testadconverter = new ADConverter();
        }


        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TestNullInitialization()
        {

            raspberryPi.initialize(null);
            raspberryPi.reset();

            Assert.IsNull(raspberryPi.Control.GPIOinterface);
            Assert.IsNull(raspberryPi.Control.LCD);
            Assert.IsNull(raspberryPi.Control.Potentiometer);
            Assert.IsNull(raspberryPi.Control.Multiplexer);
            Assert.IsNull(raspberryPi.Control.ADConverter);
        }

        [TestMethod]
        public void TestIsInitialized()
        {
            raspberryPi.reset();
            raspberryPi.initialize(TestGpiooInterface, TestLcdDisplay, Testpotentiometer, Testmultiplexer, Testadconverter);

            Assert.AreEqual(TestGpiooInterface, raspberryPi.Control.GPIOinterface);
            Assert.AreEqual(TestLcdDisplay, raspberryPi.Control.LCD);
            Assert.AreEqual(Testpotentiometer, raspberryPi.Control.Potentiometer);
            Assert.AreEqual(Testmultiplexer, raspberryPi.Control.Multiplexer);
            Assert.AreEqual(Testadconverter, raspberryPi.Control.ADConverter);

            raspberryPi.reset();
            raspberryPi.initialize(testDuplicateComponents);

            Assert.AreEqual(testDuplicateComponents[0], raspberryPi.Control.GPIOinterface);
            Assert.AreEqual(null, raspberryPi.Control.LCD);
            Assert.AreEqual(null, raspberryPi.Control.Potentiometer);
            Assert.AreEqual(testDuplicateComponents[1], raspberryPi.Control.Multiplexer);
            Assert.AreEqual(testDuplicateComponents[3], raspberryPi.Control.ADConverter);
        }

        [TestCleanup]
        public void tearDown()
        {
            raspberryPi.reset();
            raspberryPi = null;
            TestGpiooInterface = null;
            TestLcdDisplay = null;
            Testpotentiometer = null;
            Testadconverter = null;
        }


    }
}
