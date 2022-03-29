using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using Ibanity.Apis.Client.Crypto;
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

        private IIbanityService Build(
            Uri endpoint,
            IWebProxy proxy,
            X509Certificate2 clientCertificate,
            X509Certificate2 signatureCertificate,
            string signatureCertificateId,
            string pontoConnectClientId,
            string pontoConnectClientSecret,
            ILoggerFactory loggerFactory)
        {
            var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                Proxy = proxy,
                UseProxy = proxy != null
            };

            if (clientCertificate != null)
                handler.ClientCertificates.Add(clientCertificate);

            var httpClient = new HttpClient(handler) { BaseAddress = endpoint };
            var serializer = new JsonSerializer();
            var clock = new Clock();
            var nonNullLoggerFactory = loggerFactory ?? new SimpleLoggerFactory(NullLogger.Instance);

            var signatureService = signatureCertificate == null
                ? NullHttpSignatureService.Instance
                : new HttpSignatureService(
                    nonNullLoggerFactory,
                    new Sha512Digest(),
                    new RsaSsaPssSignature(signatureCertificate),
                    clock,
                    new HttpSignatureString(endpoint),
                    signatureCertificateId);

            var apiClient = new ApiClient(
                nonNullLoggerFactory,
                httpClient,
                serializer,
                signatureService);

            var pontoConnectClient = new PontoConnectClient(
                apiClient,
                pontoConnectClientId == null
                    ? UnconfiguredTokenProvider.Instance
                    : new OAuth2TokenProvider(nonNullLoggerFactory, httpClient, clock, serializer, PontoConnectClient.UrlPrefix, pontoConnectClientId, pontoConnectClientSecret),
                pontoConnectClientId == null
                    ? UnconfiguredTokenProvider.ClientAccessInstance
                    : new OAuth2ClientAccessTokenProvider(nonNullLoggerFactory, httpClient, clock, serializer, PontoConnectClient.UrlPrefix, pontoConnectClientId, pontoConnectClientSecret));

            return new IbanityService(httpClient, pontoConnectClient);
        }

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

        IIbanityService IIbanityServiceOptionalPropertiesBuilder.Build() => Build(
            _endpoint,
            _proxy,
            _clientCertificate,
            _signatureCertificate,
            _signatureCertificateId,
            _pontoConnectClientId,
            _pontoConnectClientSecret,
            _loggerFactory);
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
