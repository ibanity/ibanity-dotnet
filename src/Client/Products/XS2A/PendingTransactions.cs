using System;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.XS2A.Models;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="IPendingTransactions" />
    public class PendingTransactions : ResourceWithParentClient<PendingTransaction, object, PendingTransactionRelationships, object, CustomerAccessToken>, IPendingTransactions
    {
        private const string GrandParentEntityName = "customer/financial-institutions";
        private const string ParentEntityName = "accounts";
        private const string EntityName = "pending-transactions";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public PendingTransactions(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { GrandParentEntityName, ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        protected override PendingTransaction Map(JsonApi.Data<PendingTransaction, object, PendingTransactionRelationships, object> data)
        {
            var result = base.Map(data);

            result.AccountId = Guid.Parse(data.Relationships.Account.Data.Id);

            return result;
        }
    }

    /// <summary>
    /// <p>This is an object representing an account pending transaction. This object will give you the details of the financial transaction, including its amount and description.</p>
    /// <p>From this object, you can link back to its account.</p>
    /// <p>The Pending transaction API endpoints are customer specific and therefore can only be accessed by providing the corresponding customer access token.</p>
    /// </summary>
    public interface IPendingTransactions
    {
    }
}
