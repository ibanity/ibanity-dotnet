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

            var token = string.IsNullOrWhiteSpace(_configuration.PontoConnectRefreshToken)
                ? await _ibanityService.PontoConnect.TokenService.GetToken(
                    _configuration.PontoConnectAuthorizationCode ?? throw new ApplicationException("Either authorization code or refresh token must be set"),
                    _configuration.PontoConnectCodeVerifier,
                    _configuration.PontoConnectRedirectUri,
                    cancellationToken)
                : await _ibanityService.PontoConnect.TokenService.GetToken(
                    _configuration.PontoConnectRefreshToken,
                    cancellationToken);
        }
    }
}
