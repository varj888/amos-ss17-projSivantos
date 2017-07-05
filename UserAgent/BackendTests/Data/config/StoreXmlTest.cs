using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryBackend;
using System.Threading.Tasks;

namespace RaspberryBackendTests
{

    [TestClass]
    public class StoreXmlTest
    {
        private RaspberryPi rasPi = RaspberryPi.Instance;
        private Hi hiFromFile;

        HWComponent[] hwComponents = new HWComponent[] { new GPIOinterface(), new LCD(), new Potentiometer(), new Multiplexer(), new ADConverter() };

        [TestInitialize]
        public void startUp()
        {
            rasPi.initialize(hwComponents);
        }

        [TestMethod]
        public void TestLoadHiCfg()
        {
            rasPi.Control.setMultiplexerConfiguration();
            hiFromFile = getFile().Result;

            Assert.AreEqual("Pure", hiFromFile.Family);

            rasPi.Control.setMultiplexerConfiguration("TestFamily", "TestModel");
            hiFromFile = getFile().Result;

            Assert.AreEqual("TestFamily", hiFromFile.Family);
        }


        [TestMethod]
        public void TestLoadReceiver()
        {
            Hi hiFromFile;

            rasPi.Control.setMultiplexerConfiguration();
            rasPi.Control.SetARDVoltage(ReceiverConfig.HighPowerLeft.Item1);

            hiFromFile = getFile().Result;

            Assert.AreEqual("Pure", hiFromFile.Family);
            Assert.AreEqual("312 702 S (DN)", hiFromFile.Model);
            Assert.AreEqual(ReceiverConfig.HighPowerLeft.Item1, hiFromFile.CurrentReceiver);

        }

        private async Task<Hi> getFile()
        {
            return await StorageHandler<Hi>.Load(StorageCfgs.FileName_HiCfg);

        }

        [TestCleanup]
        public void cleanUp()
        {
            hiFromFile = null;
        }
    }
}
