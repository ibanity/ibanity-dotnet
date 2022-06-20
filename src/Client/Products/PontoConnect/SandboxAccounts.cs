using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.PontoConnect.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <summary>
    /// <para>This is an object representing a financial institution account, a fake account you can use for test purposes in a sandbox integration.</para>
    /// <para>These sandbox accounts are available only to the related organization, and can be authorized in the Ponto dashboard.</para>
    /// <para>A financial institution account belongs to a financial institution and can have many associated financial institution transactions.</para>
    /// </summary>
    public class SandboxAccounts : ResourceWithParentClient<SandboxAccount, object, object, object>, ISandboxAccounts
    {
        private const string ParentEntityName = "sandbox/financial-institutions";
        private const string EntityName = "financial-institution-accounts";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public SandboxAccounts(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<IbanityCollection<SandboxAccount>> List(Token token, Guid financialInstitutionId, int? pageSize, Guid? pageBefore, Guid? pageAfter, CancellationToken? cancellationToken) =>
            InternalCursorBasedList(
                token ?? throw new ArgumentNullException(nameof(token)),
                new[] { financialInstitutionId },
                null,
                pageSize,
                pageBefore,
                pageAfter,
                cancellationToken);

        /// <inheritdoc />
        public Task<IbanityCollection<SandboxAccount>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalCursorBasedList(
                token ?? throw new ArgumentNullException(nameof(token)),
                continuationToken ?? throw new ArgumentNullException(nameof(continuationToken)),
                cancellationToken);

        /// <inheritdoc />
        public Task<SandboxAccount> Get(Token token, Guid financialInstitutionId, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, new[] { financialInstitutionId }, id, cancellationToken);
    }

    /// <summary>
    /// <para>This is an object representing a financial institution account, a fake account you can use for test purposes in a sandbox integration.</para>
    /// <para>These sandbox accounts are available only to the related organization, and can be authorized in the Ponto dashboard.</para>
    /// <para>A financial institution account belongs to a financial institution and can have many associated financial institution transactions.</para>
    /// </summary>
    public interface ISandboxAccounts
    {
        /// <summary>
        /// List Financial Institution Accounts
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="pageBefore">Cursor that specifies the first resource of the next page</param>
        /// <param name="pageAfter">Cursor that specifies the last resource of the previous page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of financial institution account resources</returns>
        Task<IbanityCollection<SandboxAccount>> List(Token token, Guid financialInstitutionId, int? pageSize = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// List Financial Institution Accounts
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="continuationToken">Token referencing the page to request</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of financial institution account resources</returns>
        Task<IbanityCollection<SandboxAccount>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Financial Institution Account
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="id">Financial institution account ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The specified financial institution account resource</returns>
        Task<SandboxAccount> Get(Token token, Guid financialInstitutionId, Guid id, CancellationToken? cancellationToken = null);
    }
}
