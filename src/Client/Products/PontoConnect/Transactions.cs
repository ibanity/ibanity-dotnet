using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class Transactions : ResourceWithParentClient<Transaction, object, TransactionRelationships, object>, ITransactions
    {
        private const string ParentEntityName = "accounts";
        private const string EntityName = "transactions";

        public Transactions(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, ParentEntityName, EntityName)
        { }

        public Task<PaginatedCollection<Transaction>> List(Token token, Guid accountId, IEnumerable<Filter> filters, int? pageSize, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                accountId,
                filters,
                pageSize,
                cancellationToken);

        public Task<PaginatedCollection<Transaction>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                continuationToken ?? throw new ArgumentNullException(nameof(continuationToken)),
                cancellationToken);

        public Task<Transaction> Get(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, accountId, id, cancellationToken);

        protected override Transaction Map(Data<Transaction, object, TransactionRelationships, object> data)
        {
            var result = base.Map(data);

            result.AccountId = Guid.Parse(data.Relationships.Account.Data.Id);

            return result;
        }
    }

    public interface ITransactions
    {
        Task<PaginatedCollection<Transaction>> List(Token token, Guid accountId, IEnumerable<Filter> filters = null, int? pageSize = null, CancellationToken? cancellationToken = null);
        Task<PaginatedCollection<Transaction>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken = null);
        Task<Transaction> Get(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken = null);
    }
}
