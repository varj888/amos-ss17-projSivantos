using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryBackend;
using System.Collections.Generic;

namespace RaspberryBackendTests
{

    [TestClass]
    public class MultiplexerConfigFactoryTests
    {

        private const string _FAMILY = "Pure";
        private const string _MODEL = "312 702 M (DN)";
        private List<string> _EXPECTED_VALUES = new List<string>() { "", "RockerSW", "Ground", "Ground", "AMR", "", "AudioInput", "", "Ground", "PB" };

        [TestMethod]
        public void TestCreateMultiplexerConfig()
        {
            MultiplexerConfig conf = HiXmlParser.getMultiplexerConfig(_FAMILY, _MODEL);

            Dictionary<int, string> dic = conf.X_Pin_To_Value_Map;

            List<string> value_list = new List<string>();

            foreach (string value in dic.Values)
            {
                value_list.Add(value);
            }

            CollectionAssert.AreEqual(_EXPECTED_VALUES, value_list);
        }
    }
}
