using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.CodaboxConnect.Models;

namespace Ibanity.Apis.Client.Products.CodaboxConnect
{
    /// <inheritdoc cref="ICreditCardStatements" />
    public class CreditCardStatements : DocumentsService<CreditCardStatement>, ICreditCardStatements
    {
        private const string EntityName = "credit-card-statements";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public CreditCardStatements(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix) : base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        public Task GetCaro(ClientAccessToken token, Guid accountingOfficeId, string clientId, Guid id, Stream target, CancellationToken? cancellationToken = null) =>
            InternalGetToStream(token, new[] { accountingOfficeId.ToString(), clientId }, id, "application/vnd.caro.v1+xml", target, cancellationToken);
    }

    /// <summary>
    /// This resource allows an Accounting Software to retrieve a Payroll Statement for a client of an accounting office.
    /// </summary>
    public interface ICreditCardStatements
    {
        /// <summary>
        /// Get Credit Card Statement
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountingOfficeId">Accounting office identifier</param>
        /// <param name="clientId">Bank account statement's owner</param>
        /// <param name="id">Credit Card Statement ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a Credit Card Statement resource.</returns>
        Task<CreditCardStatement> Get(ClientAccessToken token, Guid accountingOfficeId, string clientId, Guid id, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Credit Card Statement PDF
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountingOfficeId">Accounting office identifier</param>
        /// <param name="clientId">Bank account statement's owner</param>
        /// <param name="id">Credit Card Statement ID</param>
        /// <param name="target">Destination stream where the PDF document will be written</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a PDF representation of the Credit Card Statement.</returns>
        Task GetPdf(ClientAccessToken token, Guid accountingOfficeId, string clientId, Guid id, Stream target, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Credit Card Statement in a structured format for easier booking
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountingOfficeId">Accounting office identifier</param>
        /// <param name="clientId">Bank account statement's owner</param>
        /// <param name="id">Credit Card Statement ID</param>
        /// <param name="target">Destination stream where the PDF document will be written</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns the Credit Card Statement in a structured format for easier booking.</returns>
        Task GetCaro(ClientAccessToken token, Guid accountingOfficeId, string clientId, Guid id, Stream target, CancellationToken? cancellationToken = null);
    }
}
