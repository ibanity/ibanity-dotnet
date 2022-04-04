using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Models;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class SandboxTransactions : ResourceWithParentClient<SandboxTransaction, object, object, object>, ISandboxTransactions
    {
        private const string TopLevelParentEntityName = "sandbox/financial-institutions";
        private const string ParentEntityName = "financial-institution-accounts";
        private const string EntityName = "financial-institution-transactions";

        public SandboxTransactions(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { TopLevelParentEntityName, ParentEntityName, EntityName })
        {
        }

        public Task<PaginatedCollection<SandboxTransaction>> List(Token token, Guid financialInstitutionId, Guid accountId, IEnumerable<Filter> filters, int? pageSize, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                new[] { financialInstitutionId, accountId },
                filters,
                pageSize,
                cancellationToken);

        public Task<PaginatedCollection<SandboxTransaction>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                continuationToken ?? throw new ArgumentNullException(nameof(continuationToken)),
                cancellationToken);

        public Task<SandboxTransaction> Get(Token token, Guid financialInstitutionId, Guid accountId, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, new[] { financialInstitutionId, accountId }, id, cancellationToken);

        public Task<SandboxTransaction> Create(Token token, Guid financialInstitutionId, Guid accountId, Transaction transaction, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (transaction is null)
                throw new ArgumentNullException(nameof(transaction));

            var payload = new JsonApi.Data<Transaction, object, object, object>();
            payload.Type = "financialInstitutionTransaction";
            payload.Attributes = transaction;

            return InternalCreate(token, new[] { financialInstitutionId, accountId }, payload, idempotencyKey, cancellationToken);
        }

        public Task<SandboxTransaction> Update(Token token, Guid financialInstitutionId, Guid accountId, Transaction transaction, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (transaction is null)
                throw new ArgumentNullException(nameof(transaction));

            var payload = new JsonApi.Data<Transaction, object, object, object>();
            payload.Type = "financialInstitutionTransaction";
            payload.Attributes = transaction;

            return InternalUpdate(token, new[] { financialInstitutionId, accountId }, payload, idempotencyKey, cancellationToken);
        }
    }

    public interface ISandboxTransactions
    {
        Task<PaginatedCollection<SandboxTransaction>> List(Token token, Guid financialInstitutionId, Guid accountId, IEnumerable<Filter> filters = null, int? pageSize = null, CancellationToken? cancellationToken = null);
        Task<PaginatedCollection<SandboxTransaction>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken = null);
        Task<SandboxTransaction> Get(Token token, Guid financialInstitutionId, Guid accountId, Guid id, CancellationToken? cancellationToken = null);
        Task<SandboxTransaction> Create(Token token, Guid financialInstitutionId, Guid accountId, Transaction transaction, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
        Task<SandboxTransaction> Update(Token token, Guid financialInstitutionId, Guid accountId, Transaction transaction, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
    }
}
