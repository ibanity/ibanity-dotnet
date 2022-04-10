using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Models;
using Ibanity.Apis.Client.Products.PontoConnect.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class SandboxAccounts : ResourceWithParentClient<SandboxAccount, object, object, object>, ISandboxAccounts
    {
        private const string ParentEntityName = "sandbox/financial-institutions";
        private const string EntityName = "financial-institution-accounts";

        public SandboxAccounts(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        public Task<PaginatedCollection<SandboxAccount>> List(Token token, Guid financialInstitutionId, IEnumerable<Filter> filters, int? pageSize, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                new[] { financialInstitutionId },
                filters,
                pageSize,
                cancellationToken);

        public Task<PaginatedCollection<SandboxAccount>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                continuationToken ?? throw new ArgumentNullException(nameof(continuationToken)),
                cancellationToken);

        public Task<SandboxAccount> Get(Token token, Guid financialInstitutionId, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, new[] { financialInstitutionId }, id, cancellationToken);
    }

    public interface ISandboxAccounts
    {
        Task<PaginatedCollection<SandboxAccount>> List(Token token, Guid financialInstitutionId, IEnumerable<Filter> filters = null, int? pageSize = null, CancellationToken? cancellationToken = null);
        Task<PaginatedCollection<SandboxAccount>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken = null);
        Task<SandboxAccount> Get(Token token, Guid financialInstitutionId, Guid id, CancellationToken? cancellationToken = null);
    }
}
