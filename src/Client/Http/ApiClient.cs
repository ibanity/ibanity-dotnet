using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IHttpSignatureService _signatureService;

        public ApiClient(HttpClient httpClient, ISerializer<string> serializer, IHttpSignatureService signatureService)
        {
            _httpClient = httpClient;
            _serializer = serializer;
            _signatureService = signatureService;
        }

        public async Task<T> Get<T>(string path, string bearerToken, CancellationToken cancellationToken)
        {
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

                var response = (await _httpClient.SendAsync(request, cancellationToken)).EnsureSuccessStatusCode();
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
