using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.IsabelConnect.Models;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc />
    public class Customers : ResourceClient<Customer, object, object, object, CustomerAccessToken>, ICustomers
    {
        private const string EntityName = "customer";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Customers(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        public Task<Customer> Delete(CustomerAccessToken token, CancellationToken? cancellationToken = null) =>
            InternalDelete(token, cancellationToken);
    }

    /// <summary>
    /// <p>This is an object representing a customer. A customer resource is created with the creation of a related customer access token.</p>
    /// <p>In the case that the contractual relationship between you and your customer is terminated, you should probably use the Delete Customer endpoint to erase ALL customer personal data.</p>
    /// <p>In the case that your customer wants to revoke your access to some accounts, you should use the Delete Account endpoint instead.</p>
    /// </summary>
    public interface ICustomers
    {
        /// <summary>
        /// Remove all customer personal data.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A resource with the deleted customer's identifier.</returns>
        Task<Customer> Delete(CustomerAccessToken token, CancellationToken? cancellationToken = null);
    }
}
