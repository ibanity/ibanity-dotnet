using System;
using System.Net.Http;
using Ibanity.Apis.Client.Products.PontoConnect;

namespace Ibanity.Apis.Client
{
    public class IbanityService : IIbanityService
    {
        private readonly HttpClient _httpClient;
        private bool disposedValue;

        public IbanityService(HttpClient httpClient, IPontoConnectClient pontoConnectClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            PontoConnect = pontoConnectClient ?? throw new ArgumentNullException(nameof(pontoConnectClient));
        }

        public IPontoConnectClient PontoConnect { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue)
                return;

            if (disposing)
                _httpClient.Dispose();

            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public interface IIbanityService : IDisposable
    {
        IPontoConnectClient PontoConnect { get; }
    }
}
