using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Models;
using Ibanity.Apis.Client.Products.XS2A.Models;
using Ibanity.Apis.Client.Utils;

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
        public Task<IbanityCollection<FinancialInstitutionResponse>> List(IEnumerable<Filter> filters = null, int? pageSize = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null) =>
            InternalCursorBasedList(
                null,
                filters,
                pageSize,
                pageBefore,
                pageAfter,
                cancellationToken);

        /// <inheritdoc />
        public Task<PageBasedXS2ACollection<FinancialInstitutionResponse>> List(IEnumerable<Filter> filters = null, long? pageNumber = null, int? pageSize = null, CancellationToken? cancellationToken = null) =>
            InternalXs2aPageBasedList(null, filters, null, pageNumber, pageSize, cancellationToken);

        /// <inheritdoc />
        public Task<IbanityCollection<FinancialInstitutionResponse>> ListForCustomer(CustomerAccessToken token, IEnumerable<Filter> filters = null, int? pageSize = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null) =>
            InternalCursorBasedList(
                token ?? throw new ArgumentNullException(nameof(token)),
                filters,
                pageSize,
                pageBefore,
                pageAfter,
                cancellationToken);

        /// <inheritdoc />
        public Task<PageBasedXS2ACollection<FinancialInstitutionResponse>> ListForCustomer(CustomerAccessToken token, IEnumerable<Filter> filters = null, long? pageNumber = null, int? pageSize = null, CancellationToken? cancellationToken = null) =>
            InternalXs2aPageBasedList(
                token ?? throw new ArgumentNullException(nameof(token)),
                filters,
                null,
                pageNumber,
                pageSize,
                cancellationToken);

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
        /// <summary>
        /// List Financial Institutions
        /// </summary>
        /// <param name="filters">Attributes to be filtered from the results</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="pageBefore">Cursor that specifies the first resource of the next page</param>
        /// <param name="pageAfter">Cursor that specifies the last resource of the previous page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of financial institution resources</returns>
        Task<IbanityCollection<FinancialInstitutionResponse>> List(IEnumerable<Filter> filters = null, int? pageSize = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// List Financial Institutions
        /// </summary>
        /// <param name="filters">Attributes to be filtered from the results</param>
        /// <param name="pageNumber">Cursor that specifies the last resource of the previous page</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of financial institution resources</returns>
        Task<PageBasedXS2ACollection<FinancialInstitutionResponse>> List(IEnumerable<Filter> filters = null, long? pageNumber = null, int? pageSize = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// List Financial Institutions for a customer
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="filters">Attributes to be filtered from the results</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="pageBefore">Cursor that specifies the first resource of the next page</param>
        /// <param name="pageAfter">Cursor that specifies the last resource of the previous page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of financial institution resources</returns>
        Task<IbanityCollection<FinancialInstitutionResponse>> ListForCustomer(CustomerAccessToken token, IEnumerable<Filter> filters = null, int? pageSize = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// List Financial Institutions for a customer
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="filters">Attributes to be filtered from the results</param>
        /// <param name="pageNumber">Cursor that specifies the last resource of the previous page</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of financial institution resources</returns>
        Task<PageBasedXS2ACollection<FinancialInstitutionResponse>> ListForCustomer(CustomerAccessToken token, IEnumerable<Filter> filters = null, long? pageNumber = null, int? pageSize = null, CancellationToken? cancellationToken = null);
    }
}
