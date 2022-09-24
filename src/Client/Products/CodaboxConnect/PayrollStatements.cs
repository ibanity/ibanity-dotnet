using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.CodaboxConnect.Models;

namespace Ibanity.Apis.Client.Products.CodaboxConnect
{
    /// <inheritdoc cref="IPayrollStatements" />
    public class PayrollStatements : GuidIdentifiedDocumentsService<PayrollStatement>, IPayrollStatements
    {
        private const string EntityName = "payroll-statements";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public PayrollStatements(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix) : base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }
    }

    /// <summary>
    /// This resource allows an Accounting Software to retrieve a Payroll Statement for a client of an accounting office.
    /// </summary>
    public interface IPayrollStatements
    {
        /// <summary>
        /// Get Payroll Statement
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountingOfficeId">Accounting office identifier</param>
        /// <param name="clientId">Payroll statement's owner</param>
        /// <param name="id">Payroll Statement ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a Payroll Statement resource.</returns>
        Task<PayrollStatement> Get(ClientAccessToken token, Guid accountingOfficeId, string clientId, Guid id, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Payroll Statement PDF
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountingOfficeId">Accounting office identifier</param>
        /// <param name="clientId">Payroll statement's owner</param>
        /// <param name="id">Payroll Statement ID</param>
        /// <param name="target">Destination stream where the PDF document will be written</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a PDF representation of the Payroll Statement.</returns>
        Task GetPdf(ClientAccessToken token, Guid accountingOfficeId, string clientId, Guid id, Stream target, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Payroll Statement metadata
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountingOfficeId">Accounting office identifier</param>
        /// <param name="clientId">Payroll statement's owner</param>
        /// <param name="id">Payroll Statement ID</param>
        /// <param name="target">Destination stream where the PDF document will be written</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns the JSON metadata of the Payroll Statement.</returns>
        Task GetJsonMetadata(ClientAccessToken token, Guid accountingOfficeId, string clientId, Guid id, Stream target, CancellationToken? cancellationToken = null);
    }
}
