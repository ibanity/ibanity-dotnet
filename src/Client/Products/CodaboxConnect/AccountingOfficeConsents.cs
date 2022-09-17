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
    }

    /// <summary>
    /// This resource allows an Accounting Software to create a new Accounting Office Consent. This consent allows an Accounting Software to retrieve the documents of clients of an Accounting Office.
    /// </summary>
    public interface IAccountingOfficeConsents
    {
    }
}
