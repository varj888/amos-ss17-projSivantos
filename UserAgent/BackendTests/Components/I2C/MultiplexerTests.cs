using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryBackend;

namespace RaspberryBackendTests
{
    [TestClass]
    public class MultiplexerTests
    {
        HWComponent[] hwComponents;

        private Multiplexer mux;
        private MultiplexerConfig muxCfg;
        private RaspberryPi rasPi;
        private Operation ops;


        [TestInitialize]
        public void setUp()
        {
            rasPi = RaspberryPi.Instance;

            hwComponents = new HWComponent[] { new GPIOinterface(), new LCD(), new Potentiometer(), new Multiplexer(), new ADConverter() };
            rasPi.initialize(hwComponents);

            mux = rasPi.Control.Multiplexer;
            ops = rasPi.Control;

        }

        //Tests if unknown requests creates the corresponding exception
        [TestMethod]
        public void TestSetDefaultMultiplexerConfig()
        {

            muxCfg = ops.setMultiplexerConfiguration();

            Assert.AreEqual(mux.get_Value_conntected_to_X(0), "");
            Assert.AreEqual(mux.get_Value_conntected_to_X(1), "RockerSW");
            Assert.AreEqual(mux.get_Value_conntected_to_X(2), "Ground");
            Assert.AreEqual(mux.get_Value_conntected_to_X(3), "Ground");
            Assert.AreEqual(mux.get_Value_conntected_to_X(4), "");
            Assert.AreEqual(mux.get_Value_conntected_to_X(5), "");
            Assert.AreEqual(mux.get_Value_conntected_to_X(6), "");
            Assert.AreEqual(mux.get_Value_conntected_to_X(7), "");
            Assert.AreEqual(mux.get_Value_conntected_to_X(8), "Ground");
            Assert.AreEqual(mux.get_Value_conntected_to_X(9), "PB");

            Assert.AreEqual(mux.get_Y_conntected_to_X(0), -1);
            Assert.AreEqual(mux.get_Y_conntected_to_X(1), muxCfg.Value_To_Y_Pin_Map["RockerSW"]);
            Assert.AreEqual(mux.get_Y_conntected_to_X(2), muxCfg.Value_To_Y_Pin_Map["Ground"]);
            Assert.AreEqual(mux.get_Y_conntected_to_X(3), muxCfg.Value_To_Y_Pin_Map["Ground"]);
            Assert.AreEqual(mux.get_Y_conntected_to_X(4), -1);
            Assert.AreEqual(mux.get_Y_conntected_to_X(5), -1);
            Assert.AreEqual(mux.get_Y_conntected_to_X(6), -1);
            Assert.AreEqual(mux.get_Y_conntected_to_X(7), -1);
            Assert.AreEqual(mux.get_Y_conntected_to_X(8), muxCfg.Value_To_Y_Pin_Map["Ground"]);
            Assert.AreEqual(mux.get_Y_conntected_to_X(9), muxCfg.Value_To_Y_Pin_Map["PB"]);
        }

        [TestMethod]
        public void TestSetMultiplexerConfig()
        {

            muxCfg = ops.setMultiplexerConfiguration("D9_RIC13", "702 S (DN)");

            Assert.AreEqual(mux.get_Value_conntected_to_X(0), "Ground");
            Assert.AreEqual(mux.get_Value_conntected_to_X(1), "Ground");
            Assert.AreEqual(mux.get_Value_conntected_to_X(2), "");
            Assert.AreEqual(mux.get_Value_conntected_to_X(3), "");
            Assert.AreEqual(mux.get_Value_conntected_to_X(4), "REC_DET");
            Assert.AreEqual(mux.get_Value_conntected_to_X(5), "Ground");
            Assert.AreEqual(mux.get_Value_conntected_to_X(6), "");
            Assert.AreEqual(mux.get_Value_conntected_to_X(7), "Ground");
            Assert.AreEqual(mux.get_Value_conntected_to_X(8), "");
            Assert.AreEqual(mux.get_Value_conntected_to_X(9), "RockerSW");

            Assert.AreEqual(mux.get_Y_conntected_to_X(0), muxCfg.Value_To_Y_Pin_Map["Ground"]);
            Assert.AreEqual(mux.get_Y_conntected_to_X(1), muxCfg.Value_To_Y_Pin_Map["Ground"]);
            Assert.AreEqual(mux.get_Y_conntected_to_X(2), -1);
            Assert.AreEqual(mux.get_Y_conntected_to_X(3), -1);
            Assert.AreEqual(mux.get_Y_conntected_to_X(4), muxCfg.Value_To_Y_Pin_Map["REC_DET"]);
            Assert.AreEqual(mux.get_Y_conntected_to_X(5), muxCfg.Value_To_Y_Pin_Map["Ground"]);
            Assert.AreEqual(mux.get_Y_conntected_to_X(6), -1);
            Assert.AreEqual(mux.get_Y_conntected_to_X(7), muxCfg.Value_To_Y_Pin_Map["Ground"]);
            Assert.AreEqual(mux.get_Y_conntected_to_X(8), -1);
            Assert.AreEqual(mux.get_Y_conntected_to_X(9), muxCfg.Value_To_Y_Pin_Map["RockerSW"]);
        }


        [TestCleanup]
        public void tearDown()
        {

        }
    }
}
