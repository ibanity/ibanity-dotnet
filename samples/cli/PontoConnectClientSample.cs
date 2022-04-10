using Ibanity.Apis.Client;

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

            Console.Error.WriteLine(token.RefreshToken);
        }
    }
}
