using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TestmachineFrontendTests1
{
    public class NUnitTest1
    {
        [TestFixture]
        public class UnitTest1
        {
            [Test]
            public void TestMethod1()
            {
                Assert.AreEqual(1, 1);
            }
        }
    }
}
