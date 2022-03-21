using System;
using System.Collections.Generic;
using Ibanity.Apis.Client.Crypto;
using Ibanity.Apis.Client.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ibanity.Apis.Client.Tests.Http
{
    [TestClass]
    public class HttpSignatureServiceTest
    {
        private const string IdempotencyKey = "61f02718-eeee-46e1-b5eb-e8fd6e799c2d";
        private const string ExpectedDigest = "SHA-512=pX9+OFjSGF4KFWUh8fv1Ihh4PuSb2KnyobO/hr228nkET5vRUhi0Qj2Ai5OcBXtzmzgII18sZiaEH4PoxkYqew==";

        [TestMethod]
        public void GetHttpSignatureHeadersReturnsExactlyTwoHeaders()
        {
            var headers = GetHeaders();

            Assert.IsNotNull(headers);
            Assert.AreEqual(headers.Count, 2);

            Assert.IsTrue(headers.ContainsKey("Digest"), "Missing 'Digest' header");
            Assert.IsTrue(headers.ContainsKey("Signature"), "Missing 'Signature' header");
        }

        [TestMethod]
        public void GetHttpSignatureHeadersReturnsProperDigest()
        {
            var headers = GetHeaders();

            Assert.AreEqual(ExpectedDigest, headers["Digest"]);
        }

        private IDictionary<string, string> GetHeaders()
        {
            var digest = new Mock<IDigest>();
            digest.
                Setup(d => d.Compute(It.IsAny<string>())).
                Returns(ExpectedDigest);

            var target = new HttpSignatureService(digest.Object);

            return target.GetHttpSignatureHeaders(
                "POST",
                new Uri("https://myproxy.com/xs2a/customer-access-tokens?test=1&test=2"),
                new Dictionary<string, string>
                {
                    { "Ibanity-Idempotency-key", IdempotencyKey }
                },
                @"{""msg"":""hello""}");
        }
    }
}
