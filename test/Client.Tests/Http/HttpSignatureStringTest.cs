using System;
using System.Collections.Generic;
using Ibanity.Apis.Client.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibanity.Apis.Client.Tests.Http
{
    [TestClass]
    public class HttpSignatureStringTest
    {
        private static readonly DateTimeOffset Timestamp = DateTimeOffset.Parse("2019-01-30T09:51:57.124733Z");
        private const string IdempotencyKey = "61f02718-eeee-46e1-b5eb-e8fd6e799c2d";
        private const string Url = "https://myproxy.com/xs2a/customer-access-tokens?test=1&test=2";
        private const string Digest = "SHA-512=pX9+OFjSGF4KFWUh8fv1Ihh4PuSb2KnyobO/hr228nkET5vRUhi0Qj2Ai5OcBXtzmzgII18sZiaEH4PoxkYqew==";

        private const string ExpectedString = @"(request-target): post /xs2a/customer-access-tokens?test=1&test=2
host: api.ibanity.com
digest: SHA-512=pX9+OFjSGF4KFWUh8fv1Ihh4PuSb2KnyobO/hr228nkET5vRUhi0Qj2Ai5OcBXtzmzgII18sZiaEH4PoxkYqew==
(created): 1548841917
ibanity-idempotency-key: 61f02718-eeee-46e1-b5eb-e8fd6e799c2d";

        [TestMethod]
        public void ComputeReturnsAProperString()
        {
            var target = new HttpSignatureString();
            var result = target.Compute(
                "POST",
                new Uri(Url),
                new Dictionary<string, string>
                {
                    { "Ibanity-Idempotency-key", IdempotencyKey }
                },
                Digest,
                Timestamp);

            Assert.AreEqual(ExpectedString, result);
        }
    }
}
