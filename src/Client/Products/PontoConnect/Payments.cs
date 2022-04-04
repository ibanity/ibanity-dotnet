using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class Payments : ResourceWithParentClient<PaymentResponse, object, object, PaymentLinks>, IPayments
    {
        private const string ParentEntityName = "accounts";
        private const string EntityName = "payments";

        public Payments(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        public Task<PaymentResponse> Create(Token token, Guid accountId, PaymentRequest payment, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (payment is null)
                throw new ArgumentNullException(nameof(payment));

            var payload = new JsonApi.Data<PaymentRequest, object, object, object>();
            payload.Type = "payment";
            payload.Attributes = payment;

            return InternalCreate(token, new[] { accountId }, payload, cancellationToken);
        }

        public Task<PaymentResponse> Get(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, new[] { accountId }, id, cancellationToken);

        public Task Delete(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken) =>
            InternalDelete(token, new[] { accountId }, id, cancellationToken);

        protected override PaymentResponse Map(Data<PaymentResponse, object, object, PaymentLinks> data)
        {
            var result = base.Map(data);

            result.RedirectUri = data.Links.RedirectString;

            return result;
        }
    }

    public interface IPayments
    {
        Task<PaymentResponse> Create(Token token, Guid accountId, PaymentRequest payment, CancellationToken? cancellationToken = null);
        Task<PaymentResponse> Get(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken = null);
        Task Delete(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken = null);
    }
}
