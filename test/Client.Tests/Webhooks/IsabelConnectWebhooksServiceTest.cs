using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Webhooks;
using Ibanity.Apis.Client.Webhooks.Jwt;
using Ibanity.Apis.Client.Webhooks.Models.IsabelConnect;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ibanity.Apis.Client.Tests.Webhooks
{
    [TestClass]
    public class IsabelConnectWebhooksServiceTest
    {
        private const string _payload = @"{""data"":{""id"":""550e8400-e29b-41d4-a716-446655440000"",""type"":""isabelConnect.payment.status.updated"",""attributes"":{""notificationId"":""14e2bff5-e365-4bc7-bf48-76b7bcd464e9"",""createdAt"":""2026-06-04T14:30:00.000Z""},""relationships"":{""payment"":{""data"":{""id"":""90000036388319"",""type"":""bulkPayment""}}}}}";
        private const string _signature = "eyJhbGciOiJSUzUxMiIsImtpZCI6InNhbmRib3hfZXZlbnRzX3NpZ25hdHVyZV8xIiwidHlwIjoiSldUIn0.eyJhdWQiOiI5NzlmZDRkMi1jYzFlLTQyNjUtODM1MS0yNmEwYzNlYzE4ODUiLCJkaWdlc3QiOiI0bmZuTVhEaDdSc2NIQnRqSG5xQzBubGdSNkFhN0s0WTRSeEc2b1JCT085bStTMTR2L0RmN2NGRk1udmc5VDRwYXFvcXBmZXJpRy9udk5IYTk5K3VVUT09IiwiZXhwIjoxNzg3Mjk4NjQwLCJpYXQiOjE3ODcyOTg1ODAsImlzcyI6Imh0dHBzOi8vYXBpLmliYW5pdHkuY29tIiwianRpIjoiZTllNDAzZDYtN2E5NS00YzE2LThkOWQtMWUwNmRlZmZlN2VjIn0.fAqTiOX_xNn9T4wOzkkCXYdKzPPMwwRJV8N3929DHjSUehXunH8yuaYmIujzCvtmCjCRoj2wkR7pucDDjDlzdqWxRepT01T3oW2uWW4-pOVZXz5BeS1aN3VKLmGeggKMAExxxBaUhUSDSWrKHVPnzltbN_Bo9lBTXXZrI6sJ83vWiTqC-hhrWQvw6PyFez_0XS9UISbBLipjXJqXbP1G9WssPmw_1hhv0fexCMZjyQbXH9-NtcYQaliUh3xP-0nBshvkuHLUCKPu6iBZBIYsSfT74wMrplmvDa22f5DwB5Uj5XRwPR96aqRJgHl-JyVSoS-M2NMtR-Yw4cocA1tKPw";

        private static readonly RSA _publicKey;
        private static readonly (string, string) _publicKeyNE = (
            "8zQfv8_RaWOlI4VpQJ0oV6dS3kqP3ze7WiH0AN6ByZRo502xgI1WnSlicaBErhEkOYhpdCNQlFMIX25niTp3PyhTSA1XY5QNVPyh2_rHemAmkKUup7oUqYT16raz9EVoC5VEtpUtfhWmIJ5cXgim5tZwzr97B1gxqFfJlVKZ7e0T2QTuyLsNi_mbNk0EsxDH6bKs6Kj4j9qDJ82MM82w00bmCFYIxXudaqdJ5eQ0SCbhjKqnyIdZBBB1yqFxmskAty1ISieIMf9szkNvRrLLF07BxaIsK-BnvHsdVYjkgBqhwcpIt_dNFEIVuaGC_3MbDO_oTwZ_05If69mq7d8qXw",
            "AQAB");

        private const long _oneSecondAfterTokenCreation = 1787298581; /* 2026-08-21 */

        static IsabelConnectWebhooksServiceTest()
        {
            var (n, e) = _publicKeyNE;

            byte[] GetBytes(string base64)
            {
                var s = base64.Replace('-', '+').Replace('_', '/');
                switch (s.Length % 4) { case 2: s += "=="; break; case 3: s += "="; break; }
                return Convert.FromBase64String(s);
            }

            _publicKey = RSA.Create();
            _publicKey.ImportParameters(new RSAParameters
            {
                Modulus = GetBytes(n),
                Exponent = GetBytes(e)
            });
        }

        [TestMethod]
        public void ProperTypeIsReturned()
        {
            var target = BuildService(_oneSecondAfterTokenCreation);
            var result = target.GetPayloadType(_payload);
            Assert.AreEqual("isabelConnect.payment.status.updated", result);
        }

        [TestMethod]
        public async Task PayloadIsProperlyValidatedAndDeserialized()
        {
            var target = BuildService(_oneSecondAfterTokenCreation);
            var result = await target.VerifyAndDeserialize(_payload, _signature, CancellationToken.None).ConfigureAwait(false);

            Assert.IsNotNull(result);

            switch (result)
            {
                case PaymentStatusUpdated webhookEvent:
                    Assert.AreEqual("14e2bff5-e365-4bc7-bf48-76b7bcd464e9", webhookEvent.NotificationId);
                    Assert.AreEqual("90000036388319", webhookEvent.PaymentId);
                    Assert.AreEqual(new DateTimeOffset(2026, 6, 4, 14, 30, 0, TimeSpan.Zero), webhookEvent.CreatedAt);
                    break;
                default:
                    Assert.Fail("Unexpected webhook event type");
                    break;
            }
        }

        [TestMethod]
        public async Task InvalidSignatureThrowsAnException()
        {
            var target = BuildService(_oneSecondAfterTokenCreation);

            var exception = await Assert.ThrowsExceptionAsync<InvalidSignatureException>(() => target.VerifyAndDeserialize(_payload, _signature.Replace("m", "n"), CancellationToken.None)).ConfigureAwait(false);
            Assert.IsTrue(exception.Message.ToLowerInvariant().Contains("signature"));
        }

        [TestMethod]
        public async Task TokenFromTheFutureThrowsAnException()
        {
            var target = BuildService(_oneSecondAfterTokenCreation - 3600);

            var exception = await Assert.ThrowsExceptionAsync<InvalidSignatureException>(() => target.VerifyAndDeserialize(_payload, _signature, CancellationToken.None)).ConfigureAwait(false);
            Assert.IsTrue(exception.Message.ToLowerInvariant().Contains("future"));
        }

        [TestMethod]
        public async Task ExpiredTokenThrowsAnException()
        {
            var target = BuildService(_oneSecondAfterTokenCreation + 3600);

            var exception = await Assert.ThrowsExceptionAsync<InvalidSignatureException>(() => target.VerifyAndDeserialize(_payload, _signature, CancellationToken.None)).ConfigureAwait(false);
            Assert.IsTrue(exception.Message.ToLowerInvariant().Contains("expired"));
        }

        private static WebhooksService BuildService(long now)
        {
            var jwksService = new Mock<IJwksService>();
            jwksService.
                Setup(s => s.GetPublicKey(It.IsAny<string>(), It.IsAny<CancellationToken>())).
                Returns(Task.FromResult(_publicKey));

            var clock = new Mock<IClock>();
            clock.
                Setup(c => c.Now).
                Returns(DateTimeOffset.FromUnixTimeSeconds(now));

            var serializer = new JsonSerializer();
            return new WebhooksService(
                serializer,
                jwksService.Object,
                new Rs512Verifier(
                    new Parser(serializer),
                    jwksService.Object,
                    clock.Object,
                    TimeSpan.FromSeconds(30d)));
        }
    }
}
