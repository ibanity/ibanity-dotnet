using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class BulkPayments : ResourceWithParentClient<BulkPaymentResponse, object, object, PaymentLinks>, IBulkPayments
    {
        private const string ParentEntityName = "accounts";
        private const string EntityName = "bulk-payments";

        public BulkPayments(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, ParentEntityName, EntityName)
        { }

        public Task<BulkPaymentResponse> Create(Token token, Guid accountId, BulkPaymentRequest payment, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (payment is null)
                throw new ArgumentNullException(nameof(payment));

            var payload = new JsonApi.Data<BulkPaymentRequest, object, object, object>();
            payload.Type = "bulkPayment";
            payload.Attributes = payment;

            return InternalCreate(token, accountId, payload, cancellationToken);
        }

        public Task<BulkPaymentResponse> Get(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, accountId, id, cancellationToken);

        public Task Delete(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken) =>
            InternalDelete(token, accountId, id, cancellationToken);

        protected override BulkPaymentResponse Map(Data<BulkPaymentResponse, object, object, PaymentLinks> data)
        {
            var result = base.Map(data);

            result.RedirectUri = data.Links.RedirectString;

            return result;
        }
    }

    public interface IBulkPayments
    {
        Task<BulkPaymentResponse> Create(Token token, Guid accountId, BulkPaymentRequest payment, CancellationToken? cancellationToken = null);
        Task<BulkPaymentResponse> Get(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken = null);
        Task Delete(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken = null);
    }
}
