using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryBackend;

namespace RaspberryBackendTests
{
    [TestClass]
    public class ADCDACTests
    {

        private readonly double VOLTAGE_TOO_HIGH = 3.059;
        private readonly double VOLTAGE_TOO_LOW = -4.032;
        private readonly double VOLTAGE_CORRECT = 1.321;

        private readonly double VOLTAGE_MIN = 0.0;
        private readonly double VOLTAGE_MAX = 1.5;

        ADCDAC adcdac;
        
        [TestInitialize]
        public void setUp()
        {
            adcdac = new ADCDAC();
        }


        //Tests if unknown requests creates the corresponding exception
        [TestMethod]
        public void TestClipping()
        {
            adcdac.setDACVoltage(VOLTAGE_TOO_HIGH);
            Assert.AreEqual(adcdac.getDACVoltage(), 1.5);

            adcdac.setDACVoltage(VOLTAGE_TOO_LOW);
            Assert.AreEqual(adcdac.getDACVoltage(), 0.0);
      
            adcdac.setDACVoltage(VOLTAGE_CORRECT);
            Assert.AreEqual(adcdac.getDACVoltage(), 1.321);
        }
    }
}
