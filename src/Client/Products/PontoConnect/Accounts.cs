using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Models;
using Ibanity.Apis.Client.Products.PontoConnect.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class Accounts : ResourceClient<AccountResponse, AccountMeta, object, object>, IAccounts
    {
        private const string EntityName = "accounts";

        public Accounts(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        public Task<PaginatedCollection<AccountResponse>> List(Token token, IEnumerable<Filter> filters, int? pageSize, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                filters,
                pageSize,
                cancellationToken);

        public Task<PaginatedCollection<AccountResponse>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                continuationToken ?? throw new ArgumentNullException(nameof(continuationToken)),
                cancellationToken);

        public Task<AccountResponse> Get(Token token, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, id, cancellationToken);

        public Task Revoke(Token token, Guid id, CancellationToken? cancellationToken = null) =>
            InternalDelete(token, id, cancellationToken);

        protected override AccountResponse Map(Data<AccountResponse, AccountMeta, object, object> data)
        {
            var result = base.Map(data);

            result.SynchronizedAt = data.Meta.SynchronizedAt;
            result.Availability = data.Meta.Availability;

            result.LatestSynchronization = data.Meta.LatestSynchronization.Attributes;
            result.LatestSynchronization.Id = Guid.Parse(data.Meta.LatestSynchronization.Id);

            return result;
        }
    }

    public interface IAccounts
    {
        Task<PaginatedCollection<AccountResponse>> List(Token token, IEnumerable<Filter> filters = null, int? pageSize = null, CancellationToken? cancellationToken = null);
        Task<PaginatedCollection<AccountResponse>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken = null);
        Task<AccountResponse> Get(Token token, Guid id, CancellationToken? cancellationToken = null);
        Task Revoke(Token token, Guid id, CancellationToken? cancellationToken = null);
    }
}
