using Ibanity.Apis.Client;

namespace Ibanity.Apis.Sample.CLI
{
    public class ClientSample
    {
        private PontoConnectClientSample _pontoConnectClientSample;
        private IsabelConnectClientSample _isabelConnectClientSample;
        private CodaboxConnectClientSample _codaboxConnectClientSample;

        public ClientSample(IConfiguration configuration, IIbanityService ibanityService)
        {
            _pontoConnectClientSample = new PontoConnectClientSample(configuration, ibanityService);
            _isabelConnectClientSample = new IsabelConnectClientSample(configuration, ibanityService);
            _codaboxConnectClientSample = new CodaboxConnectClientSample(ibanityService);
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            await _pontoConnectClientSample.Run(cancellationToken);
            await _isabelConnectClientSample.Run(cancellationToken);
            await _codaboxConnectClientSample.Run(cancellationToken);
        }
    }
}
