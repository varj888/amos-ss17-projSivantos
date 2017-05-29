using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryBackend.Data;
using System.Diagnostics;
using System.Collections.Generic;
using RaspberryBackend;

namespace RaspberryBackendTests.Data
{

    [TestClass]
    public class BreadboardFactoryTests
    {

        private const string _FAMILY = "Pure";
        private const string _MODEL = "312 702 M (DN)";
        private List<string> _EXPECTED_VALUES = new List<string>(){"","RockerSW","Ground","Ground","AMR","","AudioInput","","Ground","PB"};
        BreadboardFactory factory;

        [TestInitialize]
        public void setUp()
        {
            factory = new BreadboardFactory();
        }

        [TestMethod]
        public void TestCreateBreadboard()
        {
            Breadboard pure_bb = factory.createBreadboard(_FAMILY, _MODEL);

            Config conf = pure_bb.Pin_config;
            Dictionary<int,string> dic = conf.Pin_value_map;

            List<string> value_list = new List<string>();

            foreach(string value in dic.Values)
            {
                value_list.Add(value);
            }

            CollectionAssert.AreEqual(_EXPECTED_VALUES, value_list);
            Assert.AreEqual(pure_bb.Family_name, _FAMILY);
            Assert.AreEqual(pure_bb.Model_name, _MODEL);
        }

        [TestMethod]
        public void TestToString()
        {
            Debug.WriteLine(factory.ToString());
        }


        [TestCleanup]
        public void tearDown()
        {

        }
    }
}
