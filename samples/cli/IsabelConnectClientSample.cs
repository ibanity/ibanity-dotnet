using Ibanity.Apis.Client;

namespace Ibanity.Apis.Sample.CLI
{
    public class IsabelConnectClientSample
    {
        private readonly IConfiguration _configuration;
        private readonly IIbanityService _ibanityService;

        public IsabelConnectClientSample(IConfiguration configuration, IIbanityService ibanityService)
        {
            _configuration = configuration;
            _ibanityService = ibanityService;
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            Console.WriteLine("Running Isabel Connect client sample...");

            var isabelConnectService = _ibanityService.IsabelConnect;

            var token = await isabelConnectService.TokenService.GetToken(
                _configuration.IsabelConnectAuthorizationCode ?? throw new InvalidOperationException("Authorization code must be set"),
                _configuration.IsabelConnectCodeVerifier,
                _configuration.IsabelConnectRedirectUri,
                cancellationToken);

            Console.Error.WriteLine("Isabel Connect refresh token: " + token.RefreshToken);

            token = await isabelConnectService.TokenService.GetToken(token.RefreshToken, cancellationToken);

            Console.Error.WriteLine("Isabel Connect refresh token: " + token.RefreshToken);
            token.RefreshTokenUpdated += (_, e) => Console.Error.WriteLine("Isabel Connect refresh token updated: " + e.NewToken);
        }
    }
}
