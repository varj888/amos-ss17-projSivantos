using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryBackend;
using System.Collections.Generic;
using System.Diagnostics;

namespace RaspberryBackendTests
{

    [TestClass]
    public class MultiplexerConfigFactoryTests
    {

        private const string _FAMILY = "Pure";
        private const string _MODEL = "312 702 M (DN)";
        private List<string> _EXPECTED_VALUES = new List<string>() { "", "RockerSW", "Ground", "Ground", "AMR", "", "AudioInput", "", "Ground", "PB" };
        BreadboardFactory factory;

        [TestInitialize]
        public void setUp()
        {
            factory = new BreadboardFactory();
        }

        [TestMethod]
        public void TestCreateBreadboard()
        {
            Config conf = factory.getMultiplexerConfig(_FAMILY, _MODEL);

            Dictionary<int, string> dic = conf.Pin_value_map;

            List<string> value_list = new List<string>();

            foreach (string value in dic.Values)
            {
                value_list.Add(value);
            }

            CollectionAssert.AreEqual(_EXPECTED_VALUES, value_list);
        }

        [TestMethod]
        public void TestToString()
        {
            Debug.WriteLine(factory.getConfigAsString());
        }


        [TestCleanup]
        public void tearDown()
        {

        }
    }
}
