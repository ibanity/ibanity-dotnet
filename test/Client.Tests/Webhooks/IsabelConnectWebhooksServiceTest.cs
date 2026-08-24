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
        private const string _payload = @"{""data"":{""id"":""550e8400-e29b-41d4-a716-446655440000"",""type"":""isabelConnect.payment.status.updated"",""attributes"":{""notificationType"":""payment.status.updated"",""createdAt"":""2026-06-04T14:30:00.000Z""},""relationships"":{""payment"":{""data"":{""id"":""90000036388319"",""type"":""bulkPayment""}}}}}";
        private const string _signature = "eyJhbGciOiJSUzUxMiIsImtpZCI6InNhbmRib3hfZXZlbnRzX3NpZ25hdHVyZV8xIn0.eyJhdWQiOiI5NzlmZDRkMi1jYzFlLTQyNjUtODM1MS0yNmEwYzNlYzE4ODUiLCJkaWdlc3QiOiJ1SzRjWGQ2L3prQjlBdnFEYzlPd3RvYlZuYkFhVDhsUHdZVVFnanppYmovZWIxMmRzSUJUZmpFUnM0bjlKS0tDQVhSL1pJclpUMlJucXZPYlVPSlc0UT09IiwiZXhwIjoxNzg3Mjk4NjQwLCJpYXQiOjE3ODcyOTg1ODAsImlzcyI6Imh0dHBzOi8vYXBpLmliYW5pdHkuY29tIiwianRpIjoiYzIyODU4NmMtOTRkNi00ZTBiLTlkNWMtN2Q5Y2YxZTdhOThmIn0.FQPEnc17ONlIXEzBD3k1uayB6HQfSgYRSFACtVq-g5nfdPbxwPKJfYHBbGhVDRI2kE6QSkUHfGfCrJt1PGSbMvX4LxPCop_LJ5j3Bv6pQJCNsgVwvz_IoUR34K3DWbw0OOb6EcCig86zflc9ef_ourytzlEklJz6TyDDow-bzLW-KbLENk67MThGvzGi7llKjJdnaNbwZcx23qsY5bVJiWVegy6rsnnLQLOeFDvJ-hqEGzo99y2E5EqbJTPpfMrjK6gB_B-nXYlmwBqS4gSlauKScBKh9gqkpHBcfpqOgo5e9aov1x12X3krwx66F-9pAQk1W9D-V3nk50Msl6Pk_w";

        private static readonly RSA _publicKey;
        private static readonly (string, string) _publicKeyNE = (
            "qV42Q_Ge5rHPKMHW82B19G4Nh4_96qPiFz_qyCbtsIqSfg7HfcbXm2B3uoGHqHdE43NUmXGKChbWWEdpeHXogg9mm4IfCyamAWkG_ks2gKprQZFHt1pfuABw7PAWUt3RumkgS5oXxqaNWNEIsZj_GX3FbevH-C06HQ-mnoigTe0TBRqpHhygKoGIEFd1LVk-9pb7gAKNhjxbk8Kq5MVHwuazdqlz1yK40_0DxbhQazf40bhDTKJssF4Z1Rolk3pVZ67V-AEBeXjsmgPWl0iRVGqQbVDnizt892_h2ZFu4Vwk8fNJ3JsDNWBKp8QEPLwWVNfi60HVbQZBibegU_p1tQ",
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
                    Assert.AreEqual("payment.status.updated", webhookEvent.NotificationType);
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
