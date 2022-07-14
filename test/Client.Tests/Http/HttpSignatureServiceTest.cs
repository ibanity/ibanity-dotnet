using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Ibanity.Apis.Client.Crypto;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Utils.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ibanity.Apis.Client.Tests.Http
{
    [TestClass]
    public class HttpSignatureServiceTest
    {
        private static readonly DateTimeOffset Now = DateTimeOffset.Parse("2019-01-30T09:51:57.124733Z", CultureInfo.InvariantCulture);
        private const string CertificateId = "75b5d796-de5c-400a-81ce-e72371b01cbc";
        private const string IdempotencyKey = "61f02718-eeee-46e1-b5eb-e8fd6e799c2d";
        private const string ExpectedDigest = "SHA-512=pX9+OFjSGF4KFWUh8fv1Ihh4PuSb2KnyobO/hr228nkET5vRUhi0Qj2Ai5OcBXtzmzgII18sZiaEH4PoxkYqew==";
        private const string ExpectedSignature = @"keyId=""75b5d796-de5c-400a-81ce-e72371b01cbc"",created=1548841917,algorithm=""hs2019"",headers=""foo (bar)"",signature=""mock-signature""";

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

        [TestMethod]
        public void GetHttpSignatureHeadersReturnsProperSignature()
        {
            var headers = GetHeaders();

            Assert.AreEqual(ExpectedSignature, headers["Signature"]);
        }

        private static IDictionary<string, string> GetHeaders()
        {
            var loggerFactory = new Mock<ILoggerFactory>();
            loggerFactory.
                Setup(f => f.CreateLogger<HttpSignatureService>()).
                Returns(new Mock<ILogger>().Object);

            var digest = new Mock<IDigest>();
            digest.
                Setup(d => d.Compute(It.IsAny<Stream>())).
                Returns(ExpectedDigest);

            var signature = new Mock<ISignature>();
            signature.
                Setup(s => s.Sign(It.IsAny<string>())).
                Returns("mock-signature");

            var clock = new Mock<IClock>();
            clock.
                Setup(c => c.Now).
                Returns(Now);

            var signatureString = new Mock<IHttpSignatureString>();
            signatureString.
                Setup(s => s.Compute(
                    It.IsAny<string>(),
                    It.IsAny<Uri>(),
                    It.IsAny<IDictionary<string, string>>(),
                    It.IsAny<string>(),
                    It.IsAny<DateTimeOffset>())).
                Returns("mock-signature-string");

            signatureString.
                Setup(s => s.GetSignedHeaders(It.IsAny<IDictionary<string, string>>())).
                Returns(new[] { "foo", "(bar)" });

            var target = new HttpSignatureService(
                loggerFactory.Object,
                digest.Object,
                signature.Object,
                clock.Object,
                signatureString.Object,
                CertificateId);

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(@"{""msg"":""hello""}");
                writer.Flush();

                stream.Seek(0L, SeekOrigin.Begin);

                return target.GetHttpSignatureHeaders(
                    "POST",
                    new Uri("https://myproxy.com/xs2a/customer-access-tokens?test=1&test=2"),
                    new Dictionary<string, string>
                    {
                    { "Ibanity-Idempotency-key", IdempotencyKey }
                    },
                    stream);
            }
        }
    }
}
