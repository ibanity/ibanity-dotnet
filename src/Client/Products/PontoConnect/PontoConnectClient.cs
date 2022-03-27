using System;
using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class PontoConnectClient : ProductClient, IPontoConnectClient
    {
        private const string UrlPrefix = "ponto-connect";

        public PontoConnectClient(IApiClient apiClient, ITokenProvider tokenService, IClientAccessTokenProvider clientAccessTokenService)
            : base(apiClient, tokenService, clientAccessTokenService)
        {
            FinancialInstitutions = new FinancialInstitutions(apiClient, tokenService, UrlPrefix);
        }

        public IFinancialInstitutions FinancialInstitutions { get; }
    }

    public interface IPontoConnectClient : IProductClient
    {
        IFinancialInstitutions FinancialInstitutions { get; }
    }
}
