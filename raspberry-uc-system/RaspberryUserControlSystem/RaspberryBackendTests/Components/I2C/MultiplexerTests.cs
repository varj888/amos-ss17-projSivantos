using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryBackend;

namespace RaspberryBackendTests
{
    [TestClass]
    public class MultiplexerTests
    {
        Multiplexer mux;
        
        [TestInitialize]
        public void setUp()
        {
            mux = new Multiplexer();
        }


        //Tests if unknown requests creates the corresponding exception
        [TestMethod]
        public void TestY_to_X_mapping()
        {
            mux.setMultiplexerConfiguration();
        }


        [TestCleanup]
        public void tearDown()
        {
           
        }
    }
}
