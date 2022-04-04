using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class PontoConnectClient : ProductClient, IPontoConnectClient
    {
        public const string UrlPrefix = "ponto-connect";

        public PontoConnectClient(IApiClient apiClient, ITokenProvider tokenService, IClientAccessTokenProvider clientAccessTokenService)
            : base(apiClient, tokenService, clientAccessTokenService)
        {
            FinancialInstitutions = new FinancialInstitutions(apiClient, tokenService, UrlPrefix);
            Accounts = new Accounts(apiClient, tokenService, UrlPrefix);
            Transactions = new Transactions(apiClient, tokenService, UrlPrefix);
            ReauthorizationRequests = new ReauthorizationRequests(apiClient, tokenService, UrlPrefix);
            Payments = new Payments(apiClient, tokenService, UrlPrefix);
            BulkPayments = new BulkPayments(apiClient, tokenService, UrlPrefix);
            Synchronizations = new Synchronizations(apiClient, tokenService, UrlPrefix);
            Sandbox = new Sandbox(apiClient, tokenService, UrlPrefix);
            OnboardingDetails = new OnboardingDetails(apiClient, tokenService, UrlPrefix);
            UserInfo = new UserInfo(apiClient, tokenService, UrlPrefix);
        }

        public IFinancialInstitutions FinancialInstitutions { get; }
        public IAccounts Accounts { get; }
        public ITransactions Transactions { get; }
        public IReauthorizationRequests ReauthorizationRequests { get; }
        public IPayments Payments { get; }
        public IBulkPayments BulkPayments { get; }
        public ISynchronizations Synchronizations { get; }
        public ISandbox Sandbox { get; }
        public IOnboardingDetails OnboardingDetails { get; }
        public IUserInfo UserInfo { get; }
    }

    public interface IPontoConnectClient : IProductClient
    {
        IFinancialInstitutions FinancialInstitutions { get; }
        IAccounts Accounts { get; }
        ITransactions Transactions { get; }
        IReauthorizationRequests ReauthorizationRequests { get; }
        IPayments Payments { get; }
        IBulkPayments BulkPayments { get; }
        ISynchronizations Synchronizations { get; }
        ISandbox Sandbox { get; }
        IOnboardingDetails OnboardingDetails { get; }
        IUserInfo UserInfo { get; }
    }
}
