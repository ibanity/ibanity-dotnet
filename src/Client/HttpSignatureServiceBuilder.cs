using System;
using System.Security.Cryptography.X509Certificates;
using Ibanity.Apis.Client.Crypto;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Utils.Logging;

namespace Ibanity.Apis.Client
{
    public class HttpSignatureServiceBuilder : IHttpSignatureServiceEndpointBuilder, IHttpSignatureServiceCertificateBuilder, IHttpSignatureServiceOptionalPropertiesBuilder
    {
        private readonly IClock _clock;

        private Uri _endpoint;
        private string _certificateId;
        private X509Certificate2 _certificate;
        private ILoggerFactory _loggerFactory;

        public HttpSignatureServiceBuilder() { }

        internal HttpSignatureServiceBuilder(IClock clock) =>
            _clock = clock;

        public IHttpSignatureServiceCertificateBuilder SetEndpoint(Uri endpoint)
        {
            _endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            return this;
        }

        public IHttpSignatureServiceCertificateBuilder SetEndpoint(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ArgumentException($"'{nameof(endpoint)}' cannot be null or whitespace.", nameof(endpoint));

            return SetEndpoint(new Uri(endpoint));
        }

        IHttpSignatureServiceOptionalPropertiesBuilder IHttpSignatureServiceCertificateBuilder.AddCertificate(string id, X509Certificate2 certificate)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));

            _certificateId = id;
            _certificate = certificate ?? throw new ArgumentNullException(nameof(certificate));

            return this;
        }

        IHttpSignatureServiceOptionalPropertiesBuilder IHttpSignatureServiceCertificateBuilder.AddCertificate(string id, string path, string password)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));

            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"'{nameof(path)}' cannot be null or empty.", nameof(path));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException($"'{nameof(password)}' cannot be null or empty.", nameof(password));

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

    public interface IHttpSignatureServiceEndpointBuilder
    {
        IHttpSignatureServiceCertificateBuilder SetEndpoint(Uri endpoint);
        IHttpSignatureServiceCertificateBuilder SetEndpoint(string endpoint);
    }

    public interface IHttpSignatureServiceCertificateBuilder
    {
        IHttpSignatureServiceOptionalPropertiesBuilder AddCertificate(string id, X509Certificate2 certificate);
        IHttpSignatureServiceOptionalPropertiesBuilder AddCertificate(string id, string path, string password);
    }

    public interface IHttpSignatureServiceOptionalPropertiesBuilder
    {
        IHttpSignatureServiceOptionalPropertiesBuilder AddLogging(ILogger logger);
        IHttpSignatureServiceOptionalPropertiesBuilder AddLogging(ILoggerFactory loggerFactory);

        IHttpSignatureService Build();
    }
}
