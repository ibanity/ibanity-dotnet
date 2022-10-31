using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Products.XS2A.Models;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc />
    public class FinancialInstitutions : ResourceClient<FinancialInstitutionResponse, object, object, FinancialInstitutionLinks, CustomerAccessToken>, IFinancialInstitutions
    {
        private const string EntityName = "financial-institutions";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public FinancialInstitutions(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        protected override FinancialInstitutionResponse Map(Data<FinancialInstitutionResponse, object, object, FinancialInstitutionLinks> data)
        {
            var result = base.Map(data);

            result.Self = data.Links?.Self;

            return result;
        }
    }

    /// <summary>
    /// This is an object representing a financial institution, providing its basic details - including whether it is a sandbox object or not.
    /// </summary>
    /// <remarks>You can manage fake financial institutions in the sandbox using the create, update, and delete methods. Obviously, these endpoints will not work for real, live financial institutions.</remarks>
    public interface IFinancialInstitutions
    {
    }
}
