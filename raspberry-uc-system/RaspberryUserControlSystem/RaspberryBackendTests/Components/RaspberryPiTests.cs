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
        public void TestNotInitialized()
        {
            raspberryPi.reset();
            System.Diagnostics.Debug.WriteLine(raspberryPi.GPIOinterface != null ? raspberryPi.GPIOinterface.ToString() : "null");

            raspberryPi.initialize(testAllComponents);
            raspberryPi.reset();

            Assert.IsNull(raspberryPi.GPIOinterface);
            Assert.IsNull(raspberryPi.LCD);
            Assert.IsNull(raspberryPi.Potentiometer);
            Assert.IsNull(raspberryPi.Multiplexer);
            Assert.IsNull(raspberryPi.ADConverter);

            raspberryPi.initialize(testPartComponents);
            raspberryPi.reset();

            Assert.IsNull(raspberryPi.GPIOinterface);
            Assert.IsNull(raspberryPi.LCD);
            Assert.IsNull(raspberryPi.Potentiometer);
            Assert.IsNull(raspberryPi.Multiplexer);
            Assert.IsNull(raspberryPi.ADConverter);

            raspberryPi.initialize(testDuplicateComponents);
            raspberryPi.reset();

            Assert.IsNull(raspberryPi.GPIOinterface);
            Assert.IsNull(raspberryPi.LCD);
            Assert.IsNull(raspberryPi.Potentiometer);
            Assert.IsNull(raspberryPi.Multiplexer);
            Assert.IsNull(raspberryPi.ADConverter);
        }


        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TestNullInitialization()
        {

            raspberryPi.initialize(null);
            raspberryPi.reset();

            Assert.IsNull(raspberryPi.GPIOinterface);
            Assert.IsNull(raspberryPi.LCD);
            Assert.IsNull(raspberryPi.Potentiometer);
            Assert.IsNull(raspberryPi.Multiplexer);
            Assert.IsNull(raspberryPi.ADConverter);
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
            Assert.AreEqual(Testadconverter, raspberryPi.ADConverter);

            raspberryPi.reset();
            raspberryPi.initialize(testDuplicateComponents);

            Assert.AreEqual(testDuplicateComponents[0], raspberryPi.GPIOinterface);
            Assert.AreEqual(null, raspberryPi.LCD);
            Assert.AreEqual(null, raspberryPi.Potentiometer);
            Assert.AreEqual(testDuplicateComponents[1], raspberryPi.Multiplexer);
            Assert.AreEqual(testDuplicateComponents[3], raspberryPi.ADConverter);
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
