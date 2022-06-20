using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Http.OAuth2;
using Ibanity.Apis.Client.Products.IsabelConnect;
using Ibanity.Apis.Client.Products.PontoConnect;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Utils.Logging;
using Ibanity.Apis.Client.Webhooks;
using Ibanity.Apis.Client.Webhooks.Jwt;

namespace Ibanity.Apis.Client
{
    /// <summary>
    /// Builds an <see cref="IIbanityService" /> instance.
    /// </summary>
    public class IbanityServiceBuilder :
        IIbanityServiceEndpointBuilder,
        IIbanityServiceMutualTlsBuilder,
        IIbanityServiceOptionalPropertiesBuilder
    {
        private Uri _endpoint;
        private X509Certificate2 _clientCertificate;
        IWebProxy _proxy;
        private string _signatureCertificateId;
        private X509Certificate2 _signatureCertificate;
        private string _pontoConnectClientId;
        private string _pontoConnectClientSecret;
        private string _isabelConnectClientId;
        private string _isabelConnectClientSecret;
        private ILoggerFactory _loggerFactory;
        private TimeSpan? _timeout;
        private Action<HttpClient> _customizeClient;
        private Action<HttpClientHandler> _customizeHandler;
        private Action<HttpRequestMessage> _customizeRequest;
        private int? _retryCount;
        private TimeSpan? _retryBaseDelay;
        private TimeSpan? _webhooksJwksCachingDuration;
        private TimeSpan? _webhooksAllowedClockSkew;

        /// <inheritdoc />
        public IIbanityServiceMutualTlsBuilder SetEndpoint(Uri endpoint)
        {
            _endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            return this;
        }

        /// <inheritdoc />
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
                throw new ArgumentException($"'{nameof(path)}' cannot be null or whitespace.", nameof(path));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException($"'{nameof(password)}' cannot be null or whitespace.", nameof(password));

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
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            _signatureCertificateId = id;
            _signatureCertificate = certificate ?? throw new ArgumentNullException(nameof(certificate));

            return this;
        }

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceOptionalPropertiesBuilder.AddSignatureCertificate(string id, string path, string password)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"'{nameof(path)}' cannot be null or whitespace.", nameof(path));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException($"'{nameof(password)}' cannot be null or whitespace.", nameof(password));

            return ((IIbanityServiceOptionalPropertiesBuilder)this).AddSignatureCertificate(id, new X509Certificate2(path, password));
        }

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceOptionalPropertiesBuilder.AddPontoConnectOAuth2Authentication(string clientId, string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentException($"'{nameof(clientId)}' cannot be null or whitespace.", nameof(clientId));

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentException($"'{nameof(clientSecret)}' cannot be null or whitespace.", nameof(clientSecret));

            _pontoConnectClientId = clientId;
            _pontoConnectClientSecret = clientSecret;

            return this;
        }

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceOptionalPropertiesBuilder.AddIsabelConnectOAuth2Authentication(string clientId, string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentException($"'{nameof(clientId)}' cannot be null or whitespace.", nameof(clientId));

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentException($"'{nameof(clientSecret)}' cannot be null or whitespace.", nameof(clientSecret));

            _isabelConnectClientId = clientId;
            _isabelConnectClientSecret = clientSecret;

            return this;
        }

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceOptionalPropertiesBuilder.AddLogging(ILogger logger) =>
            ((IIbanityServiceOptionalPropertiesBuilder)this).AddLogging(new SimpleLoggerFactory(logger ?? throw new ArgumentNullException(nameof(logger))));

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceOptionalPropertiesBuilder.AddLogging(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            return this;
        }

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceOptionalPropertiesBuilder.SetTimeout(TimeSpan timeout)
        {
            _timeout = timeout;
            return this;
        }

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceOptionalPropertiesBuilder.CustomizeHttpClient(Action<HttpClient> customizeClient, Action<HttpClientHandler> customizeHandler, Action<HttpRequestMessage> customizeRequest)
        {
            _customizeClient = customizeClient;
            _customizeHandler = customizeHandler;
            _customizeRequest = customizeRequest;

            return this;
        }

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceOptionalPropertiesBuilder.EnableRetries(int count, TimeSpan? baseDelay)
        {
            _retryCount = count;
            _retryBaseDelay = baseDelay;

            return this;
        }

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceOptionalPropertiesBuilder.SetWebhooksJwksCachingDuration(TimeSpan timeToLive)
        {
            _webhooksJwksCachingDuration = timeToLive;

            return this;
        }

        IIbanityServiceOptionalPropertiesBuilder IIbanityServiceOptionalPropertiesBuilder.SetWebhooksAllowedClockSkew(TimeSpan allowedClockSkew)
        {
            _webhooksAllowedClockSkew = allowedClockSkew;

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

            if (_customizeHandler != null)
                _customizeHandler(handler);

            var httpClient = new HttpClient(handler) { BaseAddress = _endpoint };

            if (_timeout.HasValue)
                httpClient.Timeout = _timeout.Value;

            if (_customizeClient != null)
                _customizeClient(httpClient);

            var serializer = new JsonSerializer();
            var clock = new Clock();

            IHttpSignatureService signatureService;
            if (_signatureCertificate == null)
            {
                signatureService = NullHttpSignatureService.Instance;
            }
            else
            {
                var builder = new HttpSignatureServiceBuilder(clock).
                    SetEndpoint(_endpoint).
                    AddCertificate(_signatureCertificateId, _signatureCertificate);

                if (_loggerFactory != null)
                    builder = builder.AddLogging(_loggerFactory);

                signatureService = builder.Build();
            }

            var loggerFactory = _loggerFactory == null
                ? (ILoggerFactory)new SimpleLoggerFactory(NullLogger.Instance)
                : new LoggerFactoryNotNullDecorator(_loggerFactory);

            var v2ApiClient = BuildApiClient(httpClient, serializer, signatureService, loggerFactory, "2");
            var versionLessApiClient = BuildApiClient(httpClient, serializer, signatureService, loggerFactory, null);

            var pontoConnectClient = new PontoConnectClient(
                v2ApiClient,
                _pontoConnectClientId == null
                    ? UnconfiguredTokenProvider.InstanceWithCodeVerifier
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


            var isabelConnectClient = new IsabelConnectClient(
                v2ApiClient,
                _isabelConnectClientId == null
                    ? UnconfiguredTokenProvider.InstanceWithoutCodeVerifier
                    : new OAuth2TokenProvider(
                        loggerFactory,
                        httpClient,
                        clock,
                        serializer,
                        IsabelConnectClient.UrlPrefix,
                        _isabelConnectClientId,
                        _isabelConnectClientSecret),
                _isabelConnectClientId == null
                    ? UnconfiguredTokenProvider.ClientAccessInstance
                    : new OAuth2ClientAccessTokenProvider(
                        loggerFactory,
                        httpClient,
                        clock,
                        serializer,
                        IsabelConnectClient.UrlPrefix,
                        _isabelConnectClientId,
                        _isabelConnectClientSecret));

            IJwksService jwksService = new JwksService(versionLessApiClient);

            var webhooksJwksCachingDuration = _webhooksJwksCachingDuration ?? TimeSpan.FromSeconds(30d);
            if (webhooksJwksCachingDuration != TimeSpan.Zero)
                jwksService = new JwksServiceCachingDecorator(
                    jwksService,
                    clock,
                    webhooksJwksCachingDuration);

            var webhooksService = new WebhooksService(
                serializer,
                jwksService,
                new Rs512Verifier(
                    new Parser(serializer),
                    jwksService,
                    clock,
                    _webhooksAllowedClockSkew ?? TimeSpan.FromSeconds(30d)));

            return new IbanityService(httpClient, pontoConnectClient, isabelConnectClient, webhooksService);
        }

        private IApiClient BuildApiClient(HttpClient httpClient, JsonSerializer serializer, IHttpSignatureService signatureService, ILoggerFactory loggerFactory, string version)
        {
            var apiClient = new ApiClient(
                loggerFactory,
                httpClient,
                serializer,
                signatureService,
                version,
                _customizeRequest ?? (_ => { }));

            if (!_retryCount.HasValue)
                return apiClient;

            return new ApiClientRetryDecorator(
                loggerFactory,
                apiClient,
                _retryCount.Value,
                _retryBaseDelay ?? TimeSpan.FromSeconds(1d));
        }
    }

    /// <summary>
    /// Mandatory endpoint builder.
    /// </summary>
    public interface IIbanityServiceEndpointBuilder
    {
        /// <summary>
        /// Define Ibanity API base URI.
        /// </summary>
        /// <param name="endpoint">Ibanity API base URI</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IIbanityServiceMutualTlsBuilder SetEndpoint(Uri endpoint);

        /// <summary>
        /// Define Ibanity API base URI.
        /// </summary>
        /// <param name="endpoint">Ibanity API base URI</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IIbanityServiceMutualTlsBuilder SetEndpoint(string endpoint);
    }

    /// <summary>
    /// Mandatory mutual TLS builder.
    /// </summary>
    public interface IIbanityServiceMutualTlsBuilder
    {
        /// <summary>
        /// Disable client certificate.
        /// </summary>
        /// <returns>The builder to be used to pursue configuration</returns>
        /// <remarks>You will have to manage client certificate within your proxy server.</remarks>
        IIbanityServiceProxyBuilder DisableMutualTls();

        /// <summary>
        /// Define client certificate you generated for your application in our Developer Portal.
        /// </summary>
        /// <param name="certificate">Client certificate</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IIbanityServiceOptionalPropertiesBuilder AddClientCertificate(X509Certificate2 certificate);

        /// <summary>
        /// Define client certificate you generated for your application in our Developer Portal.
        /// </summary>
        /// <param name="path">Client certificate (PFX file) path</param>
        /// <param name="password">Client certificate passphrase</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        /// <remarks>The file is probably named <c>certificate.pfx</c>. Do not use <c>signature_certificate.pfx</c> here.</remarks>
        IIbanityServiceOptionalPropertiesBuilder AddClientCertificate(string path, string password);
    }

    /// <summary>
    /// Mandatory proxy builder.
    /// </summary>
    public interface IIbanityServiceProxyBuilder
    {
        /// <summary>
        /// Configure proxy server.
        /// </summary>
        /// <param name="proxy">Proxy instance in which you can configure address, credentials, ...</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IIbanityServiceOptionalPropertiesBuilder AddProxy(IWebProxy proxy);

        /// <summary>
        /// Configure proxy server.
        /// </summary>
        /// <param name="endpoint">Proxy server address</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IIbanityServiceOptionalPropertiesBuilder AddProxy(Uri endpoint);

        /// <summary>
        /// Configure proxy server.
        /// </summary>
        /// <param name="endpoint">Proxy server address</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IIbanityServiceOptionalPropertiesBuilder AddProxy(string endpoint);
    }

    /// <summary>
    /// Optional configuration builder.
    /// </summary>
    public interface IIbanityServiceOptionalPropertiesBuilder : IIbanityServiceProxyBuilder
    {
        /// <summary>
        /// Define signature certificate.
        /// </summary>
        /// <param name="id">Certificat ID from the Developer Portal</param>
        /// <param name="certificate">Signature certificate</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IIbanityServiceOptionalPropertiesBuilder AddSignatureCertificate(string id, X509Certificate2 certificate);

        /// <summary>
        /// Define signature certificate.
        /// </summary>
        /// <param name="id">Certificat ID from the Developer Portal</param>
        /// <param name="path">Signature certificate (PFX file) path</param>
        /// <param name="password">Signature certificate passphrase</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        /// <remarks>The file is probably named <c>signature_certificate.pfx</c>. Do not use <c>certificate.pfx</c> here.</remarks>
        IIbanityServiceOptionalPropertiesBuilder AddSignatureCertificate(string id, string path, string password);

        /// <summary>
        /// Define Ponto Connect OAuth2 credentials.
        /// </summary>
        /// <param name="clientId">Valid OAuth2 client identifier for your application</param>
        /// <param name="clientSecret">OAuth2 client secret</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IIbanityServiceOptionalPropertiesBuilder AddPontoConnectOAuth2Authentication(string clientId, string clientSecret);

        /// <summary>
        /// Define Isabel Connect OAuth2 credentials.
        /// </summary>
        /// <param name="clientId">Valid OAuth2 client identifier for your application</param>
        /// <param name="clientSecret">OAuth2 client secret</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IIbanityServiceOptionalPropertiesBuilder AddIsabelConnectOAuth2Authentication(string clientId, string clientSecret);

        /// <summary>
        /// Configure logger.
        /// </summary>
        /// <param name="logger">Logger instance that will be used across the whole library</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IIbanityServiceOptionalPropertiesBuilder AddLogging(ILogger logger);

        /// <summary>
        /// Configure logger factory.
        /// </summary>
        /// <param name="loggerFactory">Logger factory will be called in all library-created instances to get named loggers</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IIbanityServiceOptionalPropertiesBuilder AddLogging(ILoggerFactory loggerFactory);

        /// <summary>
        /// Configure HTTP client timeout.
        /// </summary>
        /// <param name="timeout">Delay before giving up a request</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IIbanityServiceOptionalPropertiesBuilder SetTimeout(TimeSpan timeout);

        /// <summary>
        /// Low-level HTTP client customizations.
        /// </summary>
        /// <param name="customizeClient">Callback in which <see cref="HttpClient" /> can be customized</param>
        /// <param name="customizeHandler">Callback in which <see cref="HttpClientHandler" /> can be customized</param>
        /// <param name="customizeRequest">Callback in which <see cref="HttpRequestMessage" /> can be customized</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IIbanityServiceOptionalPropertiesBuilder CustomizeHttpClient(Action<HttpClient> customizeClient = null, Action<HttpClientHandler> customizeHandler = null, Action<HttpRequestMessage> customizeRequest = null);

        /// <summary>
        /// Allow to retry failed requests.
        /// </summary>
        /// <param name="count">Number of retries after failed operations</param>
        /// <param name="baseDelay">Delay before a retry with exponential backoff</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        /// <remarks>Delay is increased by a 2-factor after each failure: 1 second, then 2 seconds, then 4 seconds, ...</remarks>
        IIbanityServiceOptionalPropertiesBuilder EnableRetries(int count = 5, TimeSpan? baseDelay = null);

        /// <summary>
        /// Define webhooks JWKS caching duration.
        /// </summary>
        /// <param name="timeToLive">Delay before a key is reloaded from server</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        /// <remarks>Default is 30 seconds.</remarks>
        IIbanityServiceOptionalPropertiesBuilder SetWebhooksJwksCachingDuration(TimeSpan timeToLive);

        /// <summary>
        /// Set the amount of clock skew to allow for when validate the expiration time and issued at time claims.
        /// </summary>
        /// <param name="allowedClockSkew">Leniency in date comparisons</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        /// <remarks>Default is 30 seconds.</remarks>
        IIbanityServiceOptionalPropertiesBuilder SetWebhooksAllowedClockSkew(TimeSpan allowedClockSkew);

        /// <summary>
        /// Create the signature service instance.
        /// </summary>
        /// <returns>A ready-to-use service</returns>
        IIbanityService Build();
    }
}
