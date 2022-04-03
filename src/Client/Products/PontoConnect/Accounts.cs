using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Models;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class Accounts : ResourceClient<Account, AccountMeta, object, object>, IAccounts
    {
        private const string EntityName = "accounts";

        public Accounts(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        public Task<PaginatedCollection<Account>> List(Token token, IEnumerable<Filter> filters, int? pageSize, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                filters,
                pageSize,
                cancellationToken);

        public Task<PaginatedCollection<Account>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                continuationToken ?? throw new ArgumentNullException(nameof(continuationToken)),
                cancellationToken);

        public Task<Account> Get(Token token, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, id, cancellationToken);

        public Task Revoke(Token token, Guid id, CancellationToken? cancellationToken = null) =>
            InternalDelete(token, id, cancellationToken);

        protected override Account Map(Data<Account, AccountMeta, object, object> data)
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
        Task<PaginatedCollection<Account>> List(Token token, IEnumerable<Filter> filters = null, int? pageSize = null, CancellationToken? cancellationToken = null);
        Task<PaginatedCollection<Account>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken = null);
        Task<Account> Get(Token token, Guid id, CancellationToken? cancellationToken = null);
        Task Revoke(Token token, Guid id, CancellationToken? cancellationToken = null);
    }
}