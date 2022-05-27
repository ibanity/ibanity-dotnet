using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.IsabelConnect.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.IsabelConnect
{
    /// <summary>
    /// <para>This is an object representing a customer account. This object will provide details about the account, including the reference and the currency.</para>
    /// <para>An account has related transactions and balances.</para>
    /// <para>The account API endpoints are customer specific and therefore can only be accessed by providing the corresponding access token.</para>
    /// </summary>
    public class Accounts : ResourceClient<Account, object, object, object>, IAccounts
    {
        private const string EntityName = "accounts";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Accounts(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }
    }

    /// <summary>
    /// <para>This is an object representing a customer account. This object will provide details about the account, including the reference and the currency.</para>
    /// <para>An account has related transactions and balances.</para>
    /// <para>The account API endpoints are customer specific and therefore can only be accessed by providing the corresponding access token.</para>
    /// </summary>
    public interface IAccounts
    {
    }
}
