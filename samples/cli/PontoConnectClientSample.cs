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

        public async Task Run()
        {
            Console.WriteLine("Running Ponto Connect client sample...");

            var token = await _ibanityService.PontoConnect.TokenService.GetToken(
                _configuration.PontoConnectAuthorizationCode,
                _configuration.PontoConnectCodeVerifier,
                _configuration.PontoConnectRedirectUri);
        }
    }
}
