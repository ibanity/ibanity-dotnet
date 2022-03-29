using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Http.OAuth2;
using Ibanity.Apis.Client.Products.PontoConnect;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Utils.Logging;

namespace Ibanity.Apis.Client
{
    public class IbanityServiceBuilder :
        IIbanityServiceEndpointBuilder,
        IIbanityServiceMutualTlsBuilder,
        IIbanityServiceProxyBuilder,
        IIbanityServiceOptionalPropertiesBuilder
    {
        private Uri _endpoint;
        private X509Certificate2 _clientCertificate;
        IWebProxy _proxy;
        private string _signatureCertificateId;
        private X509Certificate2 _signatureCertificate;
        private string _pontoConnectClientId;
        private string _pontoConnectClientSecret;
        private ILoggerFactory _loggerFactory;

        public IIbanityServiceMutualTlsBuilder SetEndpoint(Uri endpoint)
        {
            _endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            return this;
        }

        public IIbanityServiceMutualTlsBuilder SetEndpoint(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ArgumentException($"'{nameof(endpoint)}' cannot be null or whitespace.", nameof(endpoint));

            return SetEndpoint(new Uri(endpoint));
        }

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceMutualTlsBuilder.AddClientCertificate(X509Certificate2 certificate)
        {
            _clientCertificate = certificate ?? throw new ArgumentNullException(nameof(certificate));
            return this;
        }

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceMutualTlsBuilder.AddClientCertificate(string path, string password)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"'{nameof(path)}' cannot be null or empty.", nameof(path));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException($"'{nameof(password)}' cannot be null or empty.", nameof(password));

            return ((IIbanityServiceMutualTlsBuilder)this).AddClientCertificate(new X509Certificate2(path, password));
        }

        IIbanityServiceProxyBuilder IIbanityServiceMutualTlsBuilder.DisableMutualTls()
        {
            _clientCertificate = null;
            return this;
        }

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceProxyBuilder.AddProxy(IWebProxy proxy)
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
            return this;
        }

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceProxyBuilder.AddProxy(Uri endpoint) =>
            ((IIbanityServiceProxyBuilder)this).AddProxy(new WebProxy(endpoint ?? throw new ArgumentNullException(nameof(endpoint))));

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceProxyBuilder.AddProxy(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ArgumentException($"'{nameof(endpoint)}' cannot be null or whitespace.", nameof(endpoint));

            return ((IIbanityServiceProxyBuilder)this).AddProxy(new Uri(endpoint));
        }

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceOptionalPropertiesBuilder.AddSignatureCertificate(string id, X509Certificate2 certificate)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));

            _signatureCertificateId = id;
            _signatureCertificate = certificate ?? throw new ArgumentNullException(nameof(certificate));

            return this;
        }

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceOptionalPropertiesBuilder.AddSignatureCertificate(string id, string path, string password)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));

            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"'{nameof(path)}' cannot be null or empty.", nameof(path));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException($"'{nameof(password)}' cannot be null or empty.", nameof(password));

            return ((IIbanityServiceOptionalPropertiesBuilder)this).AddSignatureCertificate(id, new X509Certificate2(path, password));
        }

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceOptionalPropertiesBuilder.AddPontoConnectOAuth2Authentication(string clientId, string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentException($"'{nameof(clientId)}' cannot be null or empty.", nameof(clientId));

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentException($"'{nameof(clientSecret)}' cannot be null or empty.", nameof(clientSecret));

            _pontoConnectClientId = clientId;
            _pontoConnectClientSecret = clientSecret;

            return this;
        }

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceOptionalPropertiesBuilder.AddLogging(ILogger logger) =>
            ((IIbanityServiceOptionalPropertiesBuilder)this).AddLogging(new SimpleLoggerFactory(logger ?? throw new ArgumentNullException(nameof(logger))));

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceOptionalPropertiesBuilder.AddLogging(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            return this;
        }

        IIbanityService IIbanityServiceOptionalPropertiesBuilder.Build()
        {
            var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                Proxy = _proxy,
                UseProxy = _proxy != null
            };

            if (_clientCertificate != null)
                handler.ClientCertificates.Add(_clientCertificate);

            var httpClient = new HttpClient(handler) { BaseAddress = _endpoint };
            var serializer = new JsonSerializer();
            var clock = new Clock();
            var loggerFactory = _loggerFactory ?? new SimpleLoggerFactory(NullLogger.Instance);

            var signatureService = _signatureCertificate == null
                ? NullHttpSignatureService.Instance
                : new HttpSignatureServiceBuilder(clock).
                    SetEndpoint(_endpoint).
                    AddCertificate(_signatureCertificateId, _signatureCertificate).
                    AddLogging(_loggerFactory).
                    Build();

            var v1ApiClient = new ApiClient(
                loggerFactory,
                httpClient,
                serializer,
                signatureService,
                "1");

            var pontoConnectClient = new PontoConnectClient(
                v1ApiClient,
                _pontoConnectClientId == null
                    ? UnconfiguredTokenProvider.Instance
                    : new OAuth2TokenProvider(
                        loggerFactory,
                        httpClient,
                        clock,
                        serializer,
                        PontoConnectClient.UrlPrefix,
                        _pontoConnectClientId,
                        _pontoConnectClientSecret),
                _pontoConnectClientId == null
                    ? UnconfiguredTokenProvider.ClientAccessInstance
                    : new OAuth2ClientAccessTokenProvider(
                        loggerFactory,
                        httpClient,
                        clock,
                        serializer,
                        PontoConnectClient.UrlPrefix,
                        _pontoConnectClientId,
                        _pontoConnectClientSecret));

            return new IbanityService(httpClient, pontoConnectClient);
        }
    }

    public interface IIbanityServiceEndpointBuilder
    {
        IIbanityServiceMutualTlsBuilder SetEndpoint(Uri endpoint);
        IIbanityServiceMutualTlsBuilder SetEndpoint(string endpoint);
    }

    public interface IIbanityServiceMutualTlsBuilder
    {
        IIbanityServiceProxyBuilder DisableMutualTls();
        IIbanityServiceOptionalPropertiesBuilder AddClientCertificate(X509Certificate2 certificate);
        IIbanityServiceOptionalPropertiesBuilder AddClientCertificate(string path, string password);
    }

    public interface IIbanityServiceProxyBuilder
    {
        IIbanityServiceOptionalPropertiesBuilder AddProxy(IWebProxy proxy);
        IIbanityServiceOptionalPropertiesBuilder AddProxy(Uri endpoint);
        IIbanityServiceOptionalPropertiesBuilder AddProxy(string endpoint);
    }

    public interface IIbanityServiceOptionalPropertiesBuilder : IIbanityServiceProxyBuilder
    {
        IIbanityServiceOptionalPropertiesBuilder AddSignatureCertificate(string id, X509Certificate2 certificate);
        IIbanityServiceOptionalPropertiesBuilder AddSignatureCertificate(string id, string path, string password);
        IIbanityServiceOptionalPropertiesBuilder AddPontoConnectOAuth2Authentication(string clientId, string clientSecret);
        IIbanityServiceOptionalPropertiesBuilder AddLogging(ILogger logger);
        IIbanityServiceOptionalPropertiesBuilder AddLogging(ILoggerFactory loggerFactory);

        IIbanityService Build();
    }
}
