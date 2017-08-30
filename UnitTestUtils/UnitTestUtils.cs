using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrpalPhotoPortUtils.Base;
using OrpalPhotoPortUtils;

namespace UnitTestUtils
{
    [TestClass]
    public class UnitTestUtils
    {
        ICryptograph GetCryptograph()
        {
            return new Cryptograph();
        }

        [TestMethod]
        public void TestCryptograph()
        {
            ICryptograph cryptograph = GetCryptograph();

            string message = "test message 123";
            string encode = cryptograph.Encode(message);
            string decode = cryptograph.Decode(encode);

            Assert.IsTrue(message == decode);
        }
    }
}
