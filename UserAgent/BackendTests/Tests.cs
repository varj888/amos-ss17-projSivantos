using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaspberryBackend;

namespace RaspberryBackendTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestSingleton()
        {
            RequestController controller = RequestController.Instance;
            RequestController controller2 = RequestController.Instance;

            Assert.Equals(controller, controller2);
        }
    }
}
