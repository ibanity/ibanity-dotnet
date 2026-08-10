using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Products.IsabelConnect;
using Ibanity.Apis.Client.Products.IsabelConnect.Models;
using Ibanity.Apis.Client.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ibanity.Apis.Client.Tests.Products.IsabelConnect
{
    [TestClass]
    public class PaymentNotificationsTest
    {
        private const string BearerToken = "test-bearer-token";
        private const string NotificationId = "14e2bff5-e365-4bc7-bf48-76b7bcd464e9";
        private const string ExpectedListUrl = "isabel-connect/bulk-payment-initiation-requests/notifications";
        private const string ExpectedDeleteUrl = "isabel-connect/bulk-payment-initiation-requests/notifications/" + NotificationId;

        private Mock<IApiClient> _apiClient;
        private Mock<IAccessTokenProvider<Token>> _accessTokenProvider;
        private PaymentNotifications _service;

        [TestInitialize]
        public void SetUp()
        {
            _apiClient = new Mock<IApiClient>();
            _accessTokenProvider = new Mock<IAccessTokenProvider<Token>>();

            var refreshedToken = new Token { AccessToken = BearerToken };
            _accessTokenProvider
                .Setup(p => p.RefreshToken(It.IsAny<Token>(), It.IsAny<CancellationToken?>()))
                .ReturnsAsync(refreshedToken);

            _service = new PaymentNotifications(_apiClient.Object, _accessTokenProvider.Object, IsabelConnectClient.UrlPrefix);
        }

        [TestMethod]
        public async Task ListCallsCorrectUrl()
        {
            _apiClient
                .Setup(c => c.Get<Collection<PaymentNotification, object, object, object, OffsetBasedPaging>>(
                    ExpectedListUrl, BearerToken, CancellationToken.None))
                .ReturnsAsync(EmptyCollection());

            await _service.List(new Token()).ConfigureAwait(false);

            _apiClient.Verify(c => c.Get<Collection<PaymentNotification, object, object, object, OffsetBasedPaging>>(
                ExpectedListUrl, BearerToken, CancellationToken.None), Times.Once);
        }

        [TestMethod]
        public async Task ListMapsItemsCorrectly()
        {
            _apiClient
                .Setup(c => c.Get<Collection<PaymentNotification, object, object, object, OffsetBasedPaging>>(
                    ExpectedListUrl, BearerToken, CancellationToken.None))
                .ReturnsAsync(CollectionWithOneItem());

            var result = await _service.List(new Token()).ConfigureAwait(false);

            Assert.AreEqual(1, result.Items.Count);
            Assert.AreEqual(NotificationId, result.Items[0].Id);
            Assert.AreEqual("payment.status.updated", result.Items[0].NotificationType);
            Assert.AreEqual("2026-06-04T14:30:00.000Z", result.Items[0].CreatedAt);
        }

        [TestMethod]
        public async Task ListReturnsPagingMetadata()
        {
            _apiClient
                .Setup(c => c.Get<Collection<PaymentNotification, object, object, object, OffsetBasedPaging>>(
                    ExpectedListUrl, BearerToken, CancellationToken.None))
                .ReturnsAsync(CollectionWithOneItem());

            var result = await _service.List(new Token()).ConfigureAwait(false);

            Assert.AreEqual(0L, result.Offset);
            Assert.AreEqual(1L, result.Total);
        }

        [TestMethod]
        public async Task ListWithPageSizeAndOffsetAppendsQueryParams()
        {
            var urlWithParams = ExpectedListUrl + "?size=10&offset=5";
            _apiClient
                .Setup(c => c.Get<Collection<PaymentNotification, object, object, object, OffsetBasedPaging>>(
                    urlWithParams, BearerToken, CancellationToken.None))
                .ReturnsAsync(EmptyCollection());

            await _service.List(new Token(), pageOffset: 5, pageSize: 10).ConfigureAwait(false);

            _apiClient.Verify(c => c.Get<Collection<PaymentNotification, object, object, object, OffsetBasedPaging>>(
                urlWithParams, BearerToken, CancellationToken.None), Times.Once);
        }

        [TestMethod]
        public async Task ListWithNullMetaDoesNotThrow()
        {
            _apiClient
                .Setup(c => c.Get<Collection<PaymentNotification, object, object, object, OffsetBasedPaging>>(
                    ExpectedListUrl, BearerToken, CancellationToken.None))
                .ReturnsAsync(CollectionWithNullMeta());

            var result = await _service.List(new Token()).ConfigureAwait(false);

            Assert.IsNull(result.Offset);
            Assert.IsNull(result.Total);
            Assert.IsNull(result.ContinuationToken);
            Assert.AreEqual(0, result.Items.Count);
        }

        [TestMethod]
        public async Task ListWithNullPagingDoesNotThrow()
        {
            _apiClient
                .Setup(c => c.Get<Collection<PaymentNotification, object, object, object, OffsetBasedPaging>>(
                    ExpectedListUrl, BearerToken, CancellationToken.None))
                .ReturnsAsync(CollectionWithNullPaging());

            var result = await _service.List(new Token()).ConfigureAwait(false);

            Assert.IsNull(result.Offset);
            Assert.IsNull(result.Total);
            Assert.IsNull(result.ContinuationToken);
            Assert.AreEqual(0, result.Items.Count);
        }

        [TestMethod]
        public async Task DeleteCallsCorrectUrl()
        {
            _apiClient
                .Setup(c => c.Delete(ExpectedDeleteUrl, BearerToken, CancellationToken.None))
                .Returns(Task.CompletedTask);

            await _service.Delete(new Token(), NotificationId).ConfigureAwait(false);

            _apiClient.Verify(c => c.Delete(ExpectedDeleteUrl, BearerToken, CancellationToken.None), Times.Once);
        }

        private static Collection<PaymentNotification, object, object, object, OffsetBasedPaging> EmptyCollection() =>
            new Collection<PaymentNotification, object, object, object, OffsetBasedPaging>
            {
                Meta = new CollectionMeta<OffsetBasedPaging>
                {
                    Paging = new OffsetBasedPaging { Offset = 0, Total = 0 }
                }
            };

        private static Collection<PaymentNotification, object, object, object, OffsetBasedPaging> CollectionWithNullMeta() =>
            new Collection<PaymentNotification, object, object, object, OffsetBasedPaging>
            {
                Meta = null
            };

        private static Collection<PaymentNotification, object, object, object, OffsetBasedPaging> CollectionWithNullPaging() =>
            new Collection<PaymentNotification, object, object, object, OffsetBasedPaging>
            {
                Meta = new CollectionMeta<OffsetBasedPaging> { Paging = null }
            };

        private static Collection<PaymentNotification, object, object, object, OffsetBasedPaging> CollectionWithOneItem() =>
            new Collection<PaymentNotification, object, object, object, OffsetBasedPaging>
            {
                Meta = new CollectionMeta<OffsetBasedPaging>
                {
                    Paging = new OffsetBasedPaging { Offset = 0, Total = 1 }
                },
                Data = new List<Data<PaymentNotification, object, object, object>>
                {
                    new Data<PaymentNotification, object, object, object>
                    {
                        Id = NotificationId,
                        Attributes = new PaymentNotification
                        {
                            NotificationType = "payment.status.updated",
                            CreatedAt = "2026-06-04T14:30:00.000Z"
                        }
                    }
                }
            };
    }
}
