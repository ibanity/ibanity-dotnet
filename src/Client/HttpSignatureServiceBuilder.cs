using System;
using System.Security.Cryptography.X509Certificates;
using Ibanity.Apis.Client.Crypto;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Utils.Logging;

namespace Ibanity.Apis.Client
{
    /// <summary>
    /// Builds an <see cref="IHttpSignatureService" /> instance.
    /// </summary>
    public class HttpSignatureServiceBuilder : IHttpSignatureServiceEndpointBuilder, IHttpSignatureServiceCertificateBuilder, IHttpSignatureServiceOptionalPropertiesBuilder
    {
        private readonly IClock _clock;

        private Uri _endpoint;
        private string _certificateId;
        private X509Certificate2 _certificate;
        private ILoggerFactory _loggerFactory;

        internal HttpSignatureServiceBuilder(IClock clock) =>
            _clock = clock;

        /// <inheritdoc />
        public IHttpSignatureServiceCertificateBuilder SetEndpoint(Uri endpoint)
        {
            _endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            return this;
        }

        /// <inheritdoc />
        public IHttpSignatureServiceCertificateBuilder SetEndpoint(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ArgumentException($"'{nameof(endpoint)}' cannot be null or whitespace.", nameof(endpoint));

            return SetEndpoint(new Uri(endpoint));
        }

        IHttpSignatureServiceOptionalPropertiesBuilder IHttpSignatureServiceCertificateBuilder.AddCertificate(string id, X509Certificate2 certificate)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            _certificateId = id;
            _certificate = certificate ?? throw new ArgumentNullException(nameof(certificate));

            return this;
        }

        IHttpSignatureServiceOptionalPropertiesBuilder IHttpSignatureServiceCertificateBuilder.AddCertificate(string id, string path, string password)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"'{nameof(path)}' cannot be null or whitespace.", nameof(path));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException($"'{nameof(password)}' cannot be null or whitespace.", nameof(password));

            return ((IHttpSignatureServiceCertificateBuilder)this).AddCertificate(id, new X509Certificate2(path, password));
        }

        IHttpSignatureServiceOptionalPropertiesBuilder IHttpSignatureServiceOptionalPropertiesBuilder.AddLogging(ILogger logger) =>
            ((IHttpSignatureServiceOptionalPropertiesBuilder)this).AddLogging(new SimpleLoggerFactory(logger ?? throw new ArgumentNullException(nameof(logger))));

        IHttpSignatureServiceOptionalPropertiesBuilder IHttpSignatureServiceOptionalPropertiesBuilder.AddLogging(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            return this;
        }

        IHttpSignatureService IHttpSignatureServiceOptionalPropertiesBuilder.Build()
        {
            var loggerFactory = _loggerFactory == null
                ? (ILoggerFactory)new SimpleLoggerFactory(NullLogger.Instance)
                : new LoggerFactoryNotNullDecorator(_loggerFactory);

            return new HttpSignatureService(
                loggerFactory,
                new Sha512Digest(),
                new RsaSsaPssSignature(_certificate),
                _clock ?? new Clock(),
                new HttpSignatureString(_endpoint),
                _certificateId);
        }
    }

    /// <summary>
    /// Mandatory endpoint builder.
    /// </summary>
    public interface IHttpSignatureServiceEndpointBuilder
    {
        /// <summary>
        /// Define Ibanity API base URI.
        /// </summary>
        /// <param name="endpoint">Ibanity API base URI</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IHttpSignatureServiceCertificateBuilder SetEndpoint(Uri endpoint);

        /// <summary>
        /// Define Ibanity API base URI.
        /// </summary>
        /// <param name="endpoint">Ibanity API base URI</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IHttpSignatureServiceCertificateBuilder SetEndpoint(string endpoint);
    }

    /// <summary>
    /// Mandatory certificate builder.
    /// </summary>
    public interface IHttpSignatureServiceCertificateBuilder
    {
        /// <summary>
        /// Define signature certificate.
        /// </summary>
        /// <param name="id">Certificat ID from the Developer Portal</param>
        /// <param name="certificate">Signature certificate</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IHttpSignatureServiceOptionalPropertiesBuilder AddCertificate(string id, X509Certificate2 certificate);

        /// <summary>
        /// Define signature certificate.
        /// </summary>
        /// <param name="id">Certificat ID from the Developer Portal</param>
        /// <param name="path">Signature certificate path</param>
        /// <param name="password">Signature certificate passphrase</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IHttpSignatureServiceOptionalPropertiesBuilder AddCertificate(string id, string path, string password);
    }

    /// <summary>
    /// Optional configuration builder.
    /// </summary>
    public interface IHttpSignatureServiceOptionalPropertiesBuilder
    {
        /// <summary>
        /// Configure logger.
        /// </summary>
        /// <param name="logger">Logger instance that will be used within the whole library</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IHttpSignatureServiceOptionalPropertiesBuilder AddLogging(ILogger logger);

        /// <summary>
        /// Configure logger factory.
        /// </summary>
        /// <param name="loggerFactory">Logger factory will be called in all library-created instances to get named loggers</param>
        /// <returns>The builder to be used to pursue configuration</returns>
        IHttpSignatureServiceOptionalPropertiesBuilder AddLogging(ILoggerFactory loggerFactory);

        /// <summary>
        /// Create the signature service instance.
        /// </summary>
        /// <returns>A ready-to-use service</returns>
        IHttpSignatureService Build();
    }
}
