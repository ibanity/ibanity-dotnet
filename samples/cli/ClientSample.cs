using Ibanity.Apis.Client;

namespace Ibanity.Apis.Sample.CLI
{
    public class ClientSample
    {
        private PontoConnectClientSample _pontoConnectClientSample;
        private IsabelConnectClientSample _isabelConnectClientSample;

        public ClientSample(IConfiguration configuration, IIbanityService ibanityService)
        {
            _pontoConnectClientSample = new PontoConnectClientSample(configuration, ibanityService);
            _isabelConnectClientSample = new IsabelConnectClientSample(configuration, ibanityService);
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            await _pontoConnectClientSample.Run(cancellationToken);
            await _isabelConnectClientSample.Run(cancellationToken);
        }
    }
}
