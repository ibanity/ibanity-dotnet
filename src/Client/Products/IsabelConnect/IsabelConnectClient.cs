using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.IsabelConnect.Models;

namespace Ibanity.Apis.Client.Products.IsabelConnect
{
    /// <summary>
    /// Contains services for all Ponto Connect-releated resources.
    /// </summary>
    public class IsabelConnectClient : ProductClient, IIsabelConnectClient
    {
        /// <summary>
        /// Product name use as prefix in Ponto Connect URIs.
        /// </summary>
        public const string UrlPrefix = "isabel-connect";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="tokenService">Service to generate and refresh access tokens</param>
        /// <param name="clientAccessTokenService">Service to generate and refresh client access tokens.</param>
        public IsabelConnectClient(IApiClient apiClient, ITokenProvider tokenService, IClientAccessTokenProvider clientAccessTokenService)
            : base(apiClient, tokenService, clientAccessTokenService)
        {
            Accounts = new Accounts(apiClient, tokenService, UrlPrefix);
            Transactions = new Transactions(apiClient, tokenService, UrlPrefix);
        }

        /// <inheritdoc />
        public IAccounts Accounts { get; }

        /// <inheritdoc />
        public ITransactions Transactions { get; }
    }

    /// <summary>
    /// Contains services for all Ponto Connect-releated resources.
    /// </summary>
    public interface IIsabelConnectClient : IProductClient
    {
        /// <summary>
        /// <para>This is an object representing a customer account. This object will provide details about the account, including the reference and the currency.</para>
        /// <para>An account has related transactions and balances.</para>
        /// <para>The account API endpoints are customer specific and therefore can only be accessed by providing the corresponding access token.</para>
        /// </summary>
        IAccounts Accounts { get; }

        /// <summary>
        /// <para>This is an object representing an account transaction. This object will give you the details of the financial transaction, including its amount and execution date.</para>
        /// <para>Unlike an <see cref="IntradayTransaction" />, this is an end-of-day object which will not change.</para>
        /// </summary>
        ITransactions Transactions { get; }
    }
}
