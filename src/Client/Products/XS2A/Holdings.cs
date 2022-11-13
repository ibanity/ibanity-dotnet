using System;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.XS2A.Models;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="IHoldings" />
    public class Holdings : ResourceWithParentClient<Holding, object, HoldingRelationships, object, CustomerAccessToken>, IHoldings
    {
        private const string GrandParentEntityName = "customer/financial-institutions";
        private const string ParentEntityName = "accounts";
        private const string EntityName = "holdings";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Holdings(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { GrandParentEntityName, ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        protected override Holding Map(JsonApi.Data<Holding, object, HoldingRelationships, object> data)
        {
            var result = base.Map(data);

            result.AccountId = Guid.Parse(data.Relationships.Account.Data.Id);

            return result;
        }
    }

    /// <summary>
    /// <p>This is an object representing an account holding. This object will give you the details of the financial holding</p>
    /// <p>From this object, you can link back to its account.</p>
    /// <p>The Holding API endpoints are customer specific and therefore can only be accessed by providing the corresponding customer access token.</p>
    /// </summary>
    public interface IHoldings
    {
    }
}
