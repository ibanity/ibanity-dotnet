using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.IsabelConnect;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ibanity.Apis.Client.Tests.Products.IsabelConnect
{
    [TestClass]
    public class PaymentStatusesTest
    {
        private const string BearerToken = "test-bearer-token";
        private const string BulkPaymentId = "90000036388323";
        private const string ExpectedUrl = "isabel-connect/bulk-payment-initiation-requests/" + BulkPaymentId + "/payment-status";
        private const string ExpectedUrlWithNotification = ExpectedUrl + "?notificationId=a";

        private Mock<IApiClient> _apiClient;
        private Mock<IAccessTokenProvider<Token>> _accessTokenProvider;
        private PaymentStatuses _service;

        [TestInitialize]
        public void SetUp()
        {
            _apiClient = new Mock<IApiClient>();
            _accessTokenProvider = new Mock<IAccessTokenProvider<Token>>();

            var refreshedToken = new Token { AccessToken = BearerToken };
            _accessTokenProvider
                .Setup(p => p.RefreshToken(It.IsAny<Token>(), It.IsAny<CancellationToken?>()))
                .ReturnsAsync(refreshedToken);

            _service = new PaymentStatuses(_apiClient.Object, _accessTokenProvider.Object, IsabelConnectClient.UrlPrefix);
        }

        [TestMethod]
        public async Task GetCallsCorrectUrl()
        {
            _apiClient
                .Setup(c => c.GetToStream(ExpectedUrl, BearerToken, null, It.IsAny<Stream>(), CancellationToken.None))
                .Returns(Task.CompletedTask);

            using var stream = new MemoryStream();
            await _service.Get(new Token(), BulkPaymentId, stream).ConfigureAwait(false);

            _apiClient.Verify(c => c.GetToStream(ExpectedUrl, BearerToken, null, stream, CancellationToken.None), Times.Once);
        }

        [TestMethod]
        public async Task GetWithNotificationIdAppendsQueryParam()
        {
            _apiClient
                .Setup(c => c.GetToStream(ExpectedUrlWithNotification, BearerToken, null, It.IsAny<Stream>(), CancellationToken.None))
                .Returns(Task.CompletedTask);

            using var stream = new MemoryStream();
            await _service.Get(new Token(), BulkPaymentId, stream, notificationId: "a").ConfigureAwait(false);

            _apiClient.Verify(c => c.GetToStream(ExpectedUrlWithNotification, BearerToken, null, stream, CancellationToken.None), Times.Once);
        }

        [TestMethod]
        public async Task GetWithoutNotificationIdOmitsQueryParam()
        {
            _apiClient
                .Setup(c => c.GetToStream(ExpectedUrl, BearerToken, null, It.IsAny<Stream>(), CancellationToken.None))
                .Returns(Task.CompletedTask);

            using var stream = new MemoryStream();
            await _service.Get(new Token(), BulkPaymentId, stream, notificationId: null).ConfigureAwait(false);

            _apiClient.Verify(c => c.GetToStream(ExpectedUrl, BearerToken, null, stream, CancellationToken.None), Times.Once);
            _apiClient.Verify(c => c.GetToStream(It.Is<string>(s => s.Contains("notificationId")), BearerToken, null, stream, CancellationToken.None), Times.Never);
        }
    }
}
