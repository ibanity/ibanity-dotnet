using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Webhooks;
using Ibanity.Apis.Client.Webhooks.Jwt;
using Ibanity.Apis.Client.Webhooks.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ibanity.Apis.Client.Tests.Webhooks
{
    [TestClass]
    public class WebhooksServiceTest
    {
        private const string _payload = @"{""data"":{""attributes"":{""createdAt"":""2022-04-23T14:05:14.192Z"",""synchronizationSubtype"":""accountDetails""},""id"":""542893ac-cace-4b6f-9293-8879b3809878"",""relationships"":{""account"":{""data"":{""id"":""81763cf4-60af-4900-b1e0-8c95661b99e6"",""type"":""account""}},""synchronization"":{""data"":{""id"":""cec2f2d0-a40f-46ff-a02c-ee99b1c97bc7"",""type"":""synchronization""}}},""type"":""pontoConnect.synchronization.succeededWithoutChange""}}";
        private const string _signature = "eyJhbGciOiJSUzUxMiIsImtpZCI6InNhbmRib3hfZXZlbnRzX3NpZ25hdHVyZV8xIn0.eyJhdWQiOiI5NzlmZDRkMi1jYzFlLTQyNjUtODM1MS0yNmEwYzNlYzE4ODUiLCJkaWdlc3QiOiJKNExyUnRySm92aG1BYVBkNUhOWTQ0d3RGczRYMnVxa2NJeUd5OUx5cERuREMzdUlBMlVxdkgvdFd4QXZNYjROOTJhaDh0OWN6YXYxaHpQRzJzOFl2UT09IiwiZXhwIjoxNjUwNzIyOTcyLCJpYXQiOjE2NTA3MjI5MTIsImlzcyI6Imh0dHBzOi8vYXBpLmliYW5pdHkuY29tIiwianRpIjoiMmI2MDk3OGYtMjEzYS00ZDViLThjYWYtYjEwZDdjMmU0ZTAyIn0.DtnPW16E3GNFzFxZJmZoBw27VUyLpVdndoBnk4Pgp4ehayisSelt-mfCyjZdmZj86zvPE1xBkPiU76AvMtsyuuYv73rdxtuBlIcC2Z2TyC7IQGS8DeOCOhLGgehJ23kUjNgS0ru5P9vwcIuhyCOqG7v-sjBCWa2KZOQIpPjEuews9alAzUMdDQewkN4DEqDCSiBGfC2YCtCRJq4tJa8qjd0wFfNXPPMdL6xF3y1BX7KzmE1cAxTJ76mOC4HgngYHiLOEWQx02N6GYj9mUpPdPUjX228lGXQFwLCL_93g6AOVDVbkAy8XyBjbUbO1tIzGGpdV5gv7LqpTwMtVASvHUipqmspSrnfPhWyOSnGp-IYhmFQj9djDChrrLVK7mg1sGJyfAacA7Qfn33eAKBeW4l1Hah1KES_dtICi3YM986jCT5s4jfO-vwqmYhY4TtHKej_jbFH_uaof9leIhBodepHCG4bfph3g7b-die4EX53Wpw-QCQIURk2uzG3EGmEC1DRSMYG2v7fjXK_B3SQYdvNIzgvZuumTprcGjLbYUoLcC6j4lZ0xXr0Ik5upYa3d9Pgj_qEtco2BTwqLjHA8Dot_RVK-cDOGmSlLP0LCRkDHMpdh_BVQTP4T3NoeErD-HI9wHAV9Oni59H4d42fjQqyLCIBTcZj5Od9Xo9ucDO0";
        private static readonly RSA _publicKey;

        static WebhooksServiceTest()
        {
            var n = "v9qVZmotpil47Pw2NOmP11bpE5B_GtG6ICfqtm13Uusa4asf4FWedclr-kQTV2Ly5rSItq2f3RGRNyove_4TiTIbx21rwM5HP0iFhlVaqHjkr1iSmKzCFojOnTM4UwKQNROhDVDC6TWIzSafZkBacUrCX5l0PLSh2aEK8aiopu5ajYpOr8Ipjw_mbKXxBfcxtjgskbXPyEcf6xlB_Dygl9-btAvRTKiuie4qAWANTdVAgSnddjZMJxFnndZMCH1h-z4ISwphBYbwG2aZrZ7RfHnoIROxsdmKeostYtHy3gMR4_poufzFRR8lpvODd3m7lzdXKBTCvzlQYBNpmf6gmG9p08laE-h67F1GoqvuqspcvRlVpGZEzEwRIbPMAaS4_omCSj4HFZyo58PLUsAp--AD8GGFfVMyBdFhTEkr2235O5AP4UMdHuvyP-NPFCsqibqKK1GIl_Hy0UXnqg7-MCGqs4jX1k4IZZ3wDwza30f1O6tUtaOT8YXzZ2ZWnVWyMLcNx6gep8t3A7gTzEXcselrJgO6SLFRhYA0QmtIRtTwnl-8OmjEi5AJVzO0e-yiRj7g_JLEmgG3pDwmvbiXzEqkY5mPqJMB9G5qcd0SWgvvZs02_1tRRhvw0D5BTKfcEcLW9PKm8Nts1_BGSXKOhTSeQgxuw4iC63ST3dtpl-0=";
            var nBytes = Convert.FromBase64String(n.Replace('-', '+').Replace('_', '/'));

            var e = "AQAB";
            var eBytes = Convert.FromBase64String(e.Replace('-', '+').Replace('_', '/'));

            _publicKey = RSA.Create();
            _publicKey.ImportParameters(new RSAParameters { Modulus = nBytes, Exponent = eBytes });
        }

        [TestMethod]
        public void ProperTypeIsReturned()
        {
            var target = BuildService();
            var result = target.GetPayloadType(_payload);
            Assert.AreEqual("pontoConnect.synchronization.succeededWithoutChange", result);
        }

        [TestMethod]
        public async Task PayloadIsProperlyValidatedAndDeserialized()
        {
            var target = BuildService();
            var result = await target.VerifyAndDeserialize(_payload, _signature, CancellationToken.None);

            Assert.IsNotNull(result);

            switch (result)
            {
                case SynchronizationSucceededWithoutChange webhookEvent:
                    Assert.AreEqual("accountDetails", webhookEvent.Attributes.SynchronizationSubtype);
                    break;
                default:
                    Assert.Fail("Unexpected webhook event type");
                    break;
            }
        }

        [TestMethod]
        public void InvalidSignatureThrowsAnException()
        {
            var target = BuildService();

            Assert.ThrowsExceptionAsync<InvalidSignatureException>(() => target.VerifyAndDeserialize(_payload, _signature.Replace("m", "n"), CancellationToken.None));
        }

        private static WebhooksService BuildService()
        {
            var jwksService = new Mock<IJwksService>();
            jwksService.
                Setup(s => s.GetPublicKey(It.IsAny<string>(), It.IsAny<CancellationToken>())).
                Returns(Task.FromResult(_publicKey));

            var serializer = new JsonSerializer();
            return new WebhooksService(
                serializer,
                jwksService.Object,
                new Rs512Verifier(
                    new Parser(serializer),
                    jwksService.Object));
        }
    }
}
