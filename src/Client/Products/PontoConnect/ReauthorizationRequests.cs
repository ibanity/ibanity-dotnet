using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class ReauthorizationRequests : ResourceWithParentClient<ReauthorizationRequest, object, object, ReauthorizationRequestLinks>, IReauthorizationRequests
    {
        private const string ParentEntityName = "accounts";
        private const string EntityName = "transactions";

        public ReauthorizationRequests(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        public Task<ReauthorizationRequest> Create(Token token, Guid accountId, Uri redirect, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (redirect is null)
                throw new ArgumentNullException(nameof(redirect));

            var payload = new JsonApi.Data<ReauthorizationRequest, object, object, object>();
            payload.Type = "reauthorizationRequest";
            payload.Attributes.Redirect = redirect;

            return InternalCreate(token, new[] { accountId }, payload, cancellationToken);
        }

        public Task<ReauthorizationRequest> Create(Token token, Guid accountId, string redirectUri, CancellationToken? cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(redirectUri))
                throw new ArgumentException($"'{nameof(redirectUri)}' cannot be null or whitespace.", nameof(redirectUri));

            return Create(
                token,
                accountId,
                new Uri(redirectUri),
                cancellationToken);
        }

        protected override ReauthorizationRequest Map(Data<ReauthorizationRequest, object, object, ReauthorizationRequestLinks> data)
        {
            var result = base.Map(data);

            result.Redirect = data.Links.Redirect;

            return result;
        }
    }

    public interface IReauthorizationRequests
    {
        Task<ReauthorizationRequest> Create(Token token, Guid accountId, Uri redirect, CancellationToken? cancellationToken = null);
        Task<ReauthorizationRequest> Create(Token token, Guid accountId, string redirectUri, CancellationToken? cancellationToken = null);
    }
}
