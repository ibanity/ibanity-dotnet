using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Products.XS2A.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="IFinancialInstitutionCountries" />
    public class FinancialInstitutionCountries : ResourceClient<FinancialInstitutionCountry, object, object, object, string, CustomerAccessToken>, IFinancialInstitutionCountries
    {
        private const string EntityName = "financial-institution-countries";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public FinancialInstitutionCountries(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        protected override string ParseId(string id) => id;

        /// <inheritdoc />
        protected override FinancialInstitutionCountry Map(Data<FinancialInstitutionCountry, object, object, object> data)
        {
            if (data.Attributes == null)
                data.Attributes = new FinancialInstitutionCountry();

            return base.Map(data);
        }

        /// <inheritdoc />
        public Task<IbanityCollection<FinancialInstitutionCountry>> List(int? pageSize = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null) =>
            InternalCursorBasedList(null, null, pageSize, pageBefore, pageAfter, cancellationToken);
    }

    /// <summary>
    /// This endpoint provides a list of the unique countries for which there are financial institutions available in the list financial institutions endpoint. These codes can be used to filter the financial institutions by country.
    /// </summary>
    public interface IFinancialInstitutionCountries
    {
        /// <summary>
        /// List Financial Institutions Countries
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageBefore"></param>
        /// <param name="pageAfter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IbanityCollection<FinancialInstitutionCountry>> List(int? pageSize = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);
    }
}
