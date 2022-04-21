using System;
using System.Security.Cryptography.X509Certificates;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Webhooks;
using Ibanity.Apis.Client.Webhooks.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibanity.Apis.Client.Tests.Webhooks
{
    [TestClass]
    public class WebhooksServiceTest
    {
        private const string _payload = @"{""data"":{""attributes"":{""createdAt"":""2022-04-19T21:34:16.473Z"",""synchronizationSubtype"":""accountDetails""},""id"":""b5a57d6f-1823-4e50-8bef-33cf1eee3e3b"",""relationships"":{""account"":{""data"":{""id"":""81763cf4-60af-4900-b1e0-8c95661b99e6"",""type"":""account""}},""synchronization"":{""data"":{""id"":""f35596ad-8509-484e-9e93-d54fd3324f6b"",""type"":""synchronization""}}},""type"":""pontoConnect.synchronization.succeededWithoutChange""}}";
        private const string _signature = "eyJhbGciOiJSUzUxMiIsImtpZCI6InNhbmRib3hfZXZlbnRzX3NpZ25hdHVyZV8xIn0.eyJhdWQiOiI5NzlmZDRkMi1jYzFlLTQyNjUtODM1MS0yNmEwYzNlYzE4ODUiLCJkaWdlc3QiOiJhUlphWHZxd2pXZHQ2MUxnWG5FbVI4T2lUVzc0UW1UUHRseTFpSGVXZnE3T3hGemFjSTdUUzhQSE42UW9lbWhNazZIc2dMSmZ2eTljZHdqM08xZUZ0dz09IiwiZXhwIjoxNjUwNDA0MTM2LCJpYXQiOjE2NTA0MDQwNzYsImlzcyI6Imh0dHBzOi8vYXBpLmliYW5pdHkuY29tIiwianRpIjoiMzkyOTQ1YTItNTE0OS00NDU5LThjZDQtYzhlYjE1ZTRjZmUwIn0.Hk1OpsEuP_v7_YDPf7FaHUxuuZ15oY-PATCZdvswYmlXNKlqQLCebJ3q5j944b_XzxfIef6wsvewy5MozMZQQM1O2pkJoFCXkSJ21NfkRNbxhPsXX212LnfrlkFXlnT08gba-JJmoe3tqZvKKdwDjwe32DuqmEa20LXdtaaO8AwFJbfrTVcg15W7PTUMQmGs6p_nefFFApdQjHWV6f5OWnkRDvLcT4YJu4Zja3Azx5H8NBmfc_pXTG4WB9iEuSfbOTN7M88RahPO6teCRoUaN0DkDoOPoKz-3nhd-fEMNUZhGnKmLBJUDlmDlu-FgGT35bPcMVgGvKZw8HmZTWmAd8VqrFCnwzjt3M0Nb5nW52qIf5IGKT1a8MXAEtFYg1kuNGS98e1XCFvaHNWSlaH3w0JGViVDSPfRTn8eMAotexL-bsG5Wu2DsZXVfco0GDkFZuDco1tdeIN7QNlHwt0pg7e7LnZOcERUnOdPHGVhMZtnjRyKpD47w-m-pdbP2WxSTJ0WvJIClO4eZPso4afwpSxzB-7k63B8IMrDO6BiDr6M59gkK47GL2Ohh4qOpYcUUkutPVRWETumI2EF_lV_szSe2n0pOT3L5oI3GRq5QgS56aqyFqM7fwntkA3jtUZCdo6DNCoX8ruJUIXP_m3tl0deeyqj0oLLKRjCff9VcTo";

        [TestMethod]
        public void ProperTypeIsReturned()
        {
            var target = new WebhooksService(new JsonSerializer(), null);
            var result = target.GetPayloadType(_payload);
            Assert.AreEqual("pontoConnect.synchronization.succeededWithoutChange", result);
        }

        [TestMethod]
        public void PayloadIsProperlyValidatedAndDeserialized()
        {
            var certificatePath = Environment.GetEnvironmentVariable("CA_CERTIFICATE_PATH");

            if (string.IsNullOrWhiteSpace(certificatePath))
                Assert.Inconclusive("Missing 'CA_CERTIFICATE_PATH' environment variables");

            var target = new WebhooksService(new JsonSerializer(), new X509Certificate2(certificatePath));
            var result = target.ValidateAndDeserialize(_payload, _signature);

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
            var certificatePath = Environment.GetEnvironmentVariable("CA_CERTIFICATE_PATH");

            if (string.IsNullOrWhiteSpace(certificatePath))
                Assert.Inconclusive("Missing 'CA_CERTIFICATE_PATH' environment variables");

            var target = new WebhooksService(new JsonSerializer(), new X509Certificate2(certificatePath));

            Assert.ThrowsException<InvalidSignatureException>(() => target.ValidateAndDeserialize(_payload, _signature.Replace("m", "n")));
        }
    }
}
