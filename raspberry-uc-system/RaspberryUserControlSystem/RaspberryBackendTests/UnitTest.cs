
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryBackend;

namespace RaspberryBackendTests
{
    [TestClass]
    public class RequestControllerTests
    {

        [TestMethod]
        public void TestSample()
        {
            Assert.AreEqual(1, 1);
        }
        [TestMethod]
        public void TestSingleton()
        {
            //RequestController controller = RequestController.Instance;
            //RequestController controller2 = RequestController.Instance;
            //Assert.AreEqual(controller, controller2);
        }
    }
}
