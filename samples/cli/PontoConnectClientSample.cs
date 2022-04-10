using Ibanity.Apis.Client;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Sample.CLI
{
    public class PontoConnectClientSample
    {
        private readonly IConfiguration _configuration;
        private readonly IIbanityService _ibanityService;

        public PontoConnectClientSample(IConfiguration configuration, IIbanityService ibanityService)
        {
            _configuration = configuration;
            _ibanityService = ibanityService;
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            Console.WriteLine("Running Ponto Connect client sample...");

            var pontoConnectService = _ibanityService.PontoConnect;

            var token = string.IsNullOrWhiteSpace(_configuration.PontoConnectRefreshToken)
                ? await pontoConnectService.TokenService.GetToken(
                    _configuration.PontoConnectAuthorizationCode ?? throw new ApplicationException("Either authorization code or refresh token must be set"),
                    _configuration.PontoConnectCodeVerifier,
                    _configuration.PontoConnectRedirectUri,
                    cancellationToken)
                : await pontoConnectService.TokenService.GetToken(
                    _configuration.PontoConnectRefreshToken,
                    cancellationToken);

            var financialInstitutions = await pontoConnectService.FinancialInstitutions.ListForOrganization(token, cancellationToken: cancellationToken);
            foreach (var financialInstitution in financialInstitutions)
                Console.WriteLine("Financial institution: " + financialInstitution);

            while (financialInstitutions.ContinuationToken != null)
            {
                financialInstitutions = await pontoConnectService.FinancialInstitutions.ListForOrganization(token, financialInstitutions.ContinuationToken, cancellationToken);
                foreach (var financialInstitution in financialInstitutions)
                    Console.WriteLine("Financial institution: " + financialInstitution);
            }

            var userInfo = await pontoConnectService.UserInfo.Get(token, cancellationToken);
            Console.WriteLine("User information: " + userInfo);

            var sanboxService = pontoConnectService.Sandbox;

            var sandboxAccounts = await sanboxService.Accounts.List(token, financialInstitutions.First().Id, cancellationToken: cancellationToken);
            foreach (var sandboxAccount in sandboxAccounts)
                Console.WriteLine("Sandbox account: " + sandboxAccount);

            var sandboxTransactions = await sanboxService.Transactions.List(token, financialInstitutions.First().Id, sandboxAccounts.First().Id, cancellationToken: cancellationToken);
            foreach (var sandboxTransaction in sandboxTransactions)
                Console.WriteLine("Sandbox transaction: " + sandboxTransaction);

            var accounts = await pontoConnectService.Accounts.List(token, cancellationToken: cancellationToken);
            foreach (var account in accounts)
                Console.WriteLine("Account: " + account);

            var synchronization = await pontoConnectService.Synchronizations.Create(token, new SynchronizationRequest
            {
                ResourceType = "account",
                Subtype = "accountDetails",
                ResourceId = accounts.First().Id
            }, cancellationToken: cancellationToken);

            synchronization = await pontoConnectService.Synchronizations.Get(token, synchronization.Id, cancellationToken);

            Console.WriteLine($"Synchronization: " + synchronization);

            Console.Error.WriteLine(token.RefreshToken);
        }
    }
}