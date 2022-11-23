using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Webhooks;
using Ibanity.Apis.Client.Webhooks.Jwt;
using Ibanity.Apis.Client.Webhooks.Models.PontoConnect;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ibanity.Apis.Client.Tests.Webhooks
{
    [TestClass]
    public class WebhooksServiceTest
    {
        private const string _payload = @"{""data"":{""attributes"":{""createdAt"":""2022-11-23T23:10:13.953328Z""},""id"":""4730b99b-9c4c-47c5-b577-f2bf4bea1e5f"",""relationships"":{""account"":{""data"":{""id"":""81763cf4-60af-4900-b1e0-8c95661b99e6"",""type"":""account""}},""organization"":{""data"":{""id"":""a86f1396-013b-4258-857b-a8a212c540e8"",""type"":""organization""}}},""type"":""pontoConnect.integration.accountRevoked""}}";
        private const string _signature = "eyJhbGciOiJSUzUxMiIsImtpZCI6InNhbmRib3hfZXZlbnRzX3NpZ25hdHVyZV8xIn0.eyJhdWQiOiI5NzlmZDRkMi1jYzFlLTQyNjUtODM1MS0yNmEwYzNlYzE4ODUiLCJkaWdlc3QiOiJmN3RLNWprQ2lmSmhDM0R2WThKQXhPUmlPKzJJaU5sREtSQnBSWGFDNlgrL1NydVo5U3JJRCtGU0x3c09VTktJbXJGcVJqWDRZMUpNbk1QZEdseW0zUT09IiwiZXhwIjoxNjY5MjQ1MDc3LCJpYXQiOjE2NjkyNDUwMTcsImlzcyI6Imh0dHBzOi8vYXBpLmliYW5pdHkuY29tIiwianRpIjoiMTBhNWUxMjMtOGI5MS00YTk1LWE0ZTYtZWVmNTYzOTE5ODFhIn0.jisar8dKtBdo9c-T_petAUz361hyRlC5tcowGKlno8TX1ukZI0z5Gv6rYe8Y2phdO8Uj42CG1_Qf4x0wKj05-jV9UxW8EE4guow41Buij7PG3OtkgLIM2EoLUHjrkU8couQOX6DvjveZI3_SKkBUon9vOnMZbZKvMw5k_9TkW7UQbAl9vkbaagcVvZxUyWgpO427nX5lZYA9WxeMCypiGd5jkneNujsWPC_mPqR45sRz1uvGchQFlpR58LDD6UdoLee5K5lzu7jp1UBTPUJCjZb3tZYPPRpNM9UDX_0HxoW_v9hOj6ZxPOye0gn_MYAYn_0NVg9gAHIp7UxRIHjg-MDJAmT3Vf0RUekCmuHTZqj7wBPKmruAoktvOHC0TJ1K9bScvMEgnnxQnmWXU5MHH06ucMCYp9gzR3CbJSjnHs-4I2glFjZ-9ceRBiA2AUAhy9KCOrClTHVTpYN7FEcowX6ND_6zr-Y21OgpDN69h-HhJ6mvBwVitA_Y3gr0A9sbz3XFi4R10So6koc4xr1MzMfoubVuWZi8HQHTWYfvNfkRcpyFBeqSM7pF9nVQGn2QJHZY5qiC7mbudBo_oApOXNPqzCMGChj6g8Ai7GWRT-mt5HmGZZ5kjNio_oxuOOJk4SxN_PBWP2lcCMVGdKhVxmH2PFu8W5X0IzC3Jsx0T1A";

        private static readonly RSA _publicKey;
        private static readonly (string, string) _publicKeyNE = (
            "v9qVZmotpil47Pw2NOmP11bpE5B_GtG6ICfqtm13Uusa4asf4FWedclr-kQTV2Ly5rSItq2f3RGRNyove_4TiTIbx21rwM5HP0iFhlVaqHjkr1iSmKzCFojOnTM4UwKQNROhDVDC6TWIzSafZkBacUrCX5l0PLSh2aEK8aiopu5ajYpOr8Ipjw_mbKXxBfcxtjgskbXPyEcf6xlB_Dygl9-btAvRTKiuie4qAWANTdVAgSnddjZMJxFnndZMCH1h-z4ISwphBYbwG2aZrZ7RfHnoIROxsdmKeostYtHy3gMR4_poufzFRR8lpvODd3m7lzdXKBTCvzlQYBNpmf6gmG9p08laE-h67F1GoqvuqspcvRlVpGZEzEwRIbPMAaS4_omCSj4HFZyo58PLUsAp--AD8GGFfVMyBdFhTEkr2235O5AP4UMdHuvyP-NPFCsqibqKK1GIl_Hy0UXnqg7-MCGqs4jX1k4IZZ3wDwza30f1O6tUtaOT8YXzZ2ZWnVWyMLcNx6gep8t3A7gTzEXcselrJgO6SLFRhYA0QmtIRtTwnl-8OmjEi5AJVzO0e-yiRj7g_JLEmgG3pDwmvbiXzEqkY5mPqJMB9G5qcd0SWgvvZs02_1tRRhvw0D5BTKfcEcLW9PKm8Nts1_BGSXKOhTSeQgxuw4iC63ST3dtpl-0=",
            "AQAB");

        private const long _oneSecondAfterTokenCreation = 1669245018; /* 2022-11-23T23:10:17.0000000+00:00 */

        static WebhooksServiceTest()
        {
            var (n, e) = _publicKeyNE;

            byte[] GetBytes(string base64) =>
                Convert.FromBase64String(base64.Replace('-', '+').Replace('_', '/'));

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
            Assert.AreEqual("pontoConnect.integration.accountRevoked", result);
        }

        [TestMethod]
        public async Task PayloadIsProperlyValidatedAndDeserialized()
        {
            var target = BuildService(_oneSecondAfterTokenCreation);
            var result = await target.VerifyAndDeserialize(_payload, _signature, CancellationToken.None).ConfigureAwait(false);

            Assert.IsNotNull(result);

            switch (result)
            {
                case IntegrationAccountRevoked webhookEvent:
                    Assert.AreEqual(Guid.Parse("a86f1396-013b-4258-857b-a8a212c540e8"), webhookEvent.OrganizationId);
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
