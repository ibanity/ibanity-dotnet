using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Http
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        private readonly ISerializer<string> _serializer;

        public ApiClient(ISerializer<string> serializer, Uri endpoint, X509Certificate2? clientCertificate, IWebProxy? proxy)
        {
            _serializer = serializer;

            var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                UseProxy = proxy != null
            };

            if (clientCertificate != null)
                handler.ClientCertificates.Add(clientCertificate);

            if (proxy != null)
                handler.Proxy = proxy;

            _httpClient = new HttpClient(handler) { BaseAddress = endpoint };
        }

        public async Task<T?> Get<T>(string path, CancellationToken cancellationToken) where T : class
        {
            var response = (await _httpClient.GetAsync(path, cancellationToken)).EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return _serializer.Deserialize<T>(body);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _httpClient.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public interface IApiClient : IDisposable
    {
        Task<T?> Get<T>(string path, CancellationToken cancellationToken) where T : class;
    }
}
