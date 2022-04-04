using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class PaymentActivationRequests : ResourceClient<PaymentActivationRequest, object, object, PaymentActivationRequestLinks>, IPaymentActivationRequest
    {
        private const string EntityName = "payment-activation-requests";

        public PaymentActivationRequests(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        public Task<PaymentActivationRequest> Request(Token token, Uri redirect, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (redirect is null)
                throw new ArgumentNullException(nameof(redirect));

            var payload = new JsonApi.Data<PaymentActivationRequest, object, object, object>();
            payload.Type = "paymentActivationRequest";
            payload.Attributes.Redirect = redirect;

            return InternalCreate(token, payload, idempotencyKey, cancellationToken);
        }

        public Task<PaymentActivationRequest> Request(Token token, string redirectUri, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(redirectUri))
                throw new ArgumentException($"'{nameof(redirectUri)}' cannot be null or whitespace.", nameof(redirectUri));

            return Request(
                token,
                new Uri(redirectUri),
                idempotencyKey,
                cancellationToken);
        }

        protected override PaymentActivationRequest Map(Data<PaymentActivationRequest, object, object, PaymentActivationRequestLinks> data)
        {
            var result = base.Map(data);

            result.Redirect = data.Links.Redirect;

            return result;
        }
    }

    public interface IPaymentActivationRequest
    {
        Task<PaymentActivationRequest> Request(Token token, Uri redirect, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
        Task<PaymentActivationRequest> Request(Token token, string redirectUri, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
    }
}
