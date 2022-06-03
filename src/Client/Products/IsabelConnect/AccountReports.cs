using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.IsabelConnect.Models;

namespace Ibanity.Apis.Client.Products.IsabelConnect
{
    /// <summary>
    /// <para>This object provides details about an account report. From the list endpoint, you will receive a collection of the account report objects for the corresponding customer.</para>
    /// <para>Unlike other endpoints, the get endpoint will return the contents of the account report file instead of a json object. You can also find a link to the report in the account report object links.</para>
    /// </summary>
    public class AccountReports : ResourceClient<AccountReport, object, object, object, string>, IAccountReports
    {
        private const string EntityName = "account-reports";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public AccountReports(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        protected override string ParseId(string id) => id;
    }

    /// <summary>
    /// <para>This object provides details about an account report. From the list endpoint, you will receive a collection of the account report objects for the corresponding customer.</para>
    /// <para>Unlike other endpoints, the get endpoint will return the contents of the account report file instead of a json object. You can also find a link to the report in the account report object links.</para>
    /// </summary>
    public interface IAccountReports
    {
    }
}
