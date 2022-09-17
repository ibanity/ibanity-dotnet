using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.CodaboxConnect.Models;

namespace Ibanity.Apis.Client.Products.CodaboxConnect
{
    /// <inheritdoc cref="IAccountingOfficeConsents" />
    public class AccountingOfficeConsents : ResourceClient<AccountingOfficeConsentResponse, object, object, object, ClientAccessToken>, IAccountingOfficeConsents
    {
        private const string EntityName = "accounting-office-consents";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public AccountingOfficeConsents(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix) : base(apiClient, accessTokenProvider, urlPrefix, EntityName, false)
        { }

        /// <inheritdoc />
        public Task<AccountingOfficeConsentResponse> Get(ClientAccessToken token, Guid id, CancellationToken? cancellationToken = null) =>
            InternalGet(token, id, cancellationToken);

        /// <inheritdoc />
        public Task<AccountingOfficeConsentResponse> Create(ClientAccessToken token, NewAccountingOfficeConsent accountingOfficeConsent, CancellationToken? cancellationToken = null)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (accountingOfficeConsent is null)
                throw new ArgumentNullException(nameof(accountingOfficeConsent));

            var payload = new JsonApi.Data<AccountingOfficeConsent, object, object, object>
            {
                Type = "accountingOfficeConsent",
                Attributes = accountingOfficeConsent
            };

            return InternalCreate(token, payload, null, cancellationToken);
        }
    }

    /// <summary>
    /// This resource allows an Accounting Software to create a new Accounting Office Consent. This consent allows an Accounting Software to retrieve the documents of clients of an Accounting Office.
    /// </summary>
    public interface IAccountingOfficeConsents
    {
        /// <summary>
        /// Get Accounting Office Consent
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="id">Accounting Office Consent ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a Accounting Office Consent resource.</returns>
        Task<AccountingOfficeConsentResponse> Get(ClientAccessToken token, Guid id, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Create Accounting Office Consent
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountingOfficeConsent">An object representing a new Accounting Office Consent</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created Accounting Office Consent resource</returns>
        Task<AccountingOfficeConsentResponse> Create(ClientAccessToken token, NewAccountingOfficeConsent accountingOfficeConsent, CancellationToken? cancellationToken = null);
    }
}
