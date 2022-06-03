using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.IsabelConnect.Models;

namespace Ibanity.Apis.Client.Products.IsabelConnect
{
    /// <summary>
    /// <para>This is an object representing an account transaction. This object will give you the details of the financial transaction, including its amount and execution date.</para>
    /// <para>Unlike an <see cref="IntradayTransaction" />, this is an end-of-day object which will not change.</para>
    /// </summary>
    public class Transactions : ResourceWithParentClient<Transaction, object, object, object, string>, ITransactions
    {
        private const string ParentEntityName = "accounts";
        private const string EntityName = "transactions";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Transactions(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        protected override string ParseId(string id) => id;
    }

    /// <summary>
    /// <para>This is an object representing an account transaction. This object will give you the details of the financial transaction, including its amount and execution date.</para>
    /// <para>Unlike an <see cref="IntradayTransaction" />, this is an end-of-day object which will not change.</para>
    /// </summary>
    public interface ITransactions
    {
    }
}
