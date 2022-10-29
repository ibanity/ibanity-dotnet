using System;
using System.Net.Http;
using Ibanity.Apis.Client.Products.CodaboxConnect;
using Ibanity.Apis.Client.Products.eInvoicing;
using Ibanity.Apis.Client.Products.IsabelConnect;
using Ibanity.Apis.Client.Products.PontoConnect;
using Ibanity.Apis.Client.Products.XS2A;
using Ibanity.Apis.Client.Webhooks;

namespace Ibanity.Apis.Client
{
    /// <inheritdoc />
    public class IbanityService : IIbanityService
    {
        private readonly HttpClient _httpClient;
        private bool _disposedValue;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="httpClient">Low-level HTTP client</param>
        /// <param name="pontoConnectClient">Ponto Connect service</param>
        /// <param name="isabelConnectClient">Isabel Connect service</param>
        /// <param name="eInvoicingClient">eInvoicing service</param>
        /// <param name="codaboxConnectClient">Codabox Connect service</param>
        /// <param name="xs2aClient">XS2A service</param>
        /// <param name="webhooksService">Webhooks service</param>
        public IbanityService(HttpClient httpClient, IPontoConnectClient pontoConnectClient, IIsabelConnectClient isabelConnectClient, IEInvoicingClient eInvoicingClient, ICodaboxConnectClient codaboxConnectClient, IXS2AClient xs2aClient, IWebhooksService webhooksService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            PontoConnect = pontoConnectClient ?? throw new ArgumentNullException(nameof(pontoConnectClient));
            IsabelConnect = isabelConnectClient ?? throw new ArgumentNullException(nameof(isabelConnectClient));
            EInvoicing = eInvoicingClient ?? throw new ArgumentNullException(nameof(eInvoicingClient));
            CodaboxConnect = codaboxConnectClient ?? throw new ArgumentNullException(nameof(codaboxConnectClient));
            XS2A = xs2aClient ?? throw new ArgumentNullException(nameof(xs2aClient));
            Webhooks = webhooksService ?? throw new ArgumentNullException(nameof(webhooksService));
        }

        /// <inheritdoc />
        public IPontoConnectClient PontoConnect { get; }

        /// <inheritdoc />
        public IIsabelConnectClient IsabelConnect { get; }

        /// <inheritdoc />
        public IEInvoicingClient EInvoicing { get; }

        /// <inheritdoc />
        public ICodaboxConnectClient CodaboxConnect { get; }

        /// <inheritdoc />
        public IXS2AClient XS2A { get; }

        /// <inheritdoc />
        public IWebhooksService Webhooks { get; }

        /// <summary>
        /// Release used resources.
        /// </summary>
        /// <param name="disposing">Dispose managed resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue)
                return;

            if (disposing)
                _httpClient.Dispose();

            _disposedValue = true;
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

        /// <summary>
        /// Get the Isabel Connect service.
        /// </summary>
        IIsabelConnectClient IsabelConnect { get; }

        /// <summary>
        /// Get the eInvoicing service.
        /// </summary>
        IEInvoicingClient EInvoicing { get; }

        /// <summary>
        /// Get the Codabox Connect service.
        /// </summary>
        ICodaboxConnectClient CodaboxConnect { get; }

        /// <summary>
        /// Get the XS2A service.
        /// </summary>
        IXS2AClient XS2A { get; }

        /// <summary>
        /// Allows to validate and deserialize webhook payloads.
        /// </summary>
        IWebhooksService Webhooks { get; }
    }
}
