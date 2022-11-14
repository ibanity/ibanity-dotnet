using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.XS2A.Models;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="ISandboxFinancialInstitutionUsers" />
    public class SandboxFinancialInstitutionUsers : ResourceClient<SandboxFinancialInstitutionUserResponse, object, object, object, CustomerAccessToken>, ISandboxFinancialInstitutionUsers
    {
        private const string EntityName = "financial-institution-users";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public SandboxFinancialInstitutionUsers(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }
    }

    /// <summary>
    /// <p>This is an object representing a financial institution user. It is a fake financial institution customer you can create for test purposes.</p>
    /// <p>From this object, you can follow the links to its related financial institution accounts and transactions.</p>
    /// </summary>
    public interface ISandboxFinancialInstitutionUsers
    {
    }
}
