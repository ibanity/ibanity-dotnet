using System;
using System.Net.Http;
using Ibanity.Apis.Client.Products.PontoConnect;

namespace Ibanity.Apis.Client
{
    /// <inheritdoc />
    public class IbanityService : IIbanityService
    {
        private readonly HttpClient _httpClient;
        private bool disposedValue;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="httpClient">Low-level HTTP client</param>
        /// <param name="pontoConnectClient">Ponto Connect service</param>
        public IbanityService(HttpClient httpClient, IPontoConnectClient pontoConnectClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            PontoConnect = pontoConnectClient ?? throw new ArgumentNullException(nameof(pontoConnectClient));
        }

        /// <inheritdoc />
        public IPontoConnectClient PontoConnect { get; }

        /// <summary>
        /// Release used resources.
        /// </summary>
        /// <param name="disposing">Dispose managed resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue)
                return;

            if (disposing)
                _httpClient.Dispose();

            disposedValue = true;
        }

        /// <summary>
        /// Release used resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// <para>Root Ibanity service.</para>
    /// <para>It contains all the products.</para>
    /// </summary>
    public interface IIbanityService : IDisposable
    {
        /// <summary>
        /// Get the Ponto Connect service.
        /// </summary>
        IPontoConnectClient PontoConnect { get; }
    }
}
