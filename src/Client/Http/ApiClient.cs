using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Http
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ISerializer<string> _serializer;
        private readonly IHttpSignatureService _signatureService;

        public ApiClient(HttpClient httpClient, ISerializer<string> serializer, IHttpSignatureService signatureService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _signatureService = signatureService ?? throw new ArgumentNullException(nameof(signatureService));
        }

        public async Task<T> Get<T>(string path, string bearerToken, CancellationToken cancellationToken)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            var headers = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(bearerToken))
                headers["Authorization"] = $"Bearer {bearerToken}";

            var signatureHeaders = _signatureService.GetHttpSignatureHeaders(
                "GET",
                new Uri(_httpClient.BaseAddress + path),
                headers,
                null);

            using (var request = new HttpRequestMessage(HttpMethod.Get, path))
            {
                foreach (var kvp in headers.Union(signatureHeaders))
                    request.Headers.Add(kvp.Key, kvp.Value);

                var response = await (await _httpClient.SendAsync(request, cancellationToken)).ThrowOnFailure(_serializer);
                var body = await response.Content.ReadAsStringAsync();
                return _serializer.Deserialize<T>(body);
            }
        }
    }

    public interface IApiClient
    {
        Task<T> Get<T>(string path, string bearerToken, CancellationToken cancellationToken);
    }
}
