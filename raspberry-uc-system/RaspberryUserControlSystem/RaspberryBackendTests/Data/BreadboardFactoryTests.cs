using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryBackend.Data;
using System.Diagnostics;
using System.Collections.Generic;

namespace RaspberryBackendTests.Data
{

    [TestClass]
    public class BreadboardFactoryTests
    {

        BreadboardFactory factory;

        [TestInitialize]
        public void setUp()
        {
            factory = new BreadboardFactory();
        }

        [TestMethod]
        public void testXmlReader()
        {
            Debug.Write(factory.toString());
        }

        [TestCleanup]
        public void tearDown()
        {

        }
    }
}
