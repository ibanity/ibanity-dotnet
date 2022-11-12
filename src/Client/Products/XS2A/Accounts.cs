using System;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.XS2A.Models;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="IAccounts" />
    public class Accounts : ResourceWithParentClient<AccountResponse, AccountMeta, object, object, CustomerAccessToken>, IAccounts
    {
        private const string ParentEntityName = "customer/financial-institutions";
        private const string EntityName = "accounts";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Accounts(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        protected override AccountResponse Map(JsonApi.Data<AccountResponse, AccountMeta, object, object> data)
        {
            var result = base.Map(data);

            result.SynchronizedAt = data.Meta.SynchronizedAt;
            result.Availability = data.Meta.Availability;

            result.LatestSynchronization = data.Meta.LatestSynchronization.Attributes;
            result.LatestSynchronization.Id = Guid.Parse(data.Meta.LatestSynchronization.Id);

            return result;
        }
    }

    /// <summary>
    /// <p>This is an object representing a customer account. This object will provide details about the account, including the balance and the currency.</p>
    /// <p>An account has related transactions and belongs to a financial institution.</p>
    /// <p>The account API endpoints are customer specific and therefore can only be accessed by providing the corresponding customer access token.</p>
    /// </summary>
    public interface IAccounts
    {
    }
}
