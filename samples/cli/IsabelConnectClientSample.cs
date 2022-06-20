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
                _configuration.IsabelConnectRedirectUri,
                cancellationToken);

            Console.Error.WriteLine("Isabel Connect refresh token: " + token.RefreshToken);

            token = await isabelConnectService.TokenService.GetToken(token.RefreshToken, cancellationToken);

            Console.Error.WriteLine("Isabel Connect refresh token: " + token.RefreshToken);
            token.RefreshTokenUpdated += (_, e) => Console.Error.WriteLine("Isabel Connect refresh token updated: " + e.NewToken);

            var accounts = await isabelConnectService.Accounts.List(token, cancellationToken: cancellationToken);

            foreach (var account in accounts.Items)
                Console.WriteLine("Account: " + account);

            while (accounts.ContinuationToken != null)
            {
                accounts = await isabelConnectService.Accounts.List(token, accounts.ContinuationToken, cancellationToken);
                foreach (var account in accounts.Items)
                    Console.WriteLine("Account: " + account);
            }

            var firstAccount = await isabelConnectService.Accounts.Get(token, accounts.Items.First().Id, cancellationToken);
        }
    }
}
