using System.IO;
using Ibanity.Apis.Client.Crypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibanity.Apis.Client.Tests.Crypto
{
    [TestClass]
    public class Sha512DigestTest
    {
        [TestMethod]
        public void ProperDigestIsReturned()
        {
            var target = new Sha512Digest();

            string result;

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {

                writer.Write(@"{""msg"":""hello""}");
                writer.Flush();

                stream.Seek(0L, SeekOrigin.Begin);
                result = target.Compute(stream);
            }

            Assert.AreEqual(
                "SHA-512=pX9+OFjSGF4KFWUh8fv1Ihh4PuSb2KnyobO/hr228nkET5vRUhi0Qj2Ai5OcBXtzmzgII18sZiaEH4PoxkYqew==",
                result);
        }
    }
}
