using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Webhooks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibanity.Apis.Client.Tests.Webhooks
{
    [TestClass]
    public class WebhooksServiceTest
    {
        private const string _payload = @"{""data"":{""attributes"":{""createdAt"":""2022-04-19T18:40:26.982Z"",""synchronizationSubtype"":""accountDetails""},""id"":""16024603-7b0b-4811-a96e-7faec7e2c352"",""relationships"":{""account"":{""data"":{""id"":""81763cf4-60af-4900-b1e0-8c95661b99e6"",""type"":""account""}},""synchronization"":{""data"":{""id"":""588f1b82-48b5-44c4-8920-fe8f34157d78"",""type"":""synchronization""}}},""type"":""pontoConnect.synchronization.succeededWithoutChange""}}";

        [TestMethod]
        public void ProperTypeIsReturned()
        {
            var target = new WebhooksService(new JsonSerializer());
            var result = target.GetPayloadType(_payload);
            Assert.AreEqual("pontoConnect.synchronization.succeededWithoutChange", result);
        }
    }
}
