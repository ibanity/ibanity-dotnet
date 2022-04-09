namespace Ibanity.Apis.Sample.CLI
{
    public class Configuration : IConfiguration
    {
        public Configuration(
            string endpoint,
            string mtlsCertificatePath,
            string mtlsCertificatePassword,
            string signatureCertificateId,
            string signatureCertificatePath,
            string signatureCertificatePassword,
            string pontoConnectClientId,
            string pontoConnectClientSecret,
            string pontoConnectAuthorizationCode,
            string pontoConnectCodeVerifier,
            string pontoConnectRedirectUri)
        {
            Endpoint = endpoint;
            MtlsCertificatePath = mtlsCertificatePath;
            MtlsCertificatePassword = mtlsCertificatePassword;
            SignatureCertificateId = signatureCertificateId;
            SignatureCertificatePath = signatureCertificatePath;
            SignatureCertificatePassword = signatureCertificatePassword;
            PontoConnectClientId = pontoConnectClientId;
            PontoConnectClientSecret = pontoConnectClientSecret;
            PontoConnectAuthorizationCode = pontoConnectAuthorizationCode;
            PontoConnectCodeVerifier = pontoConnectCodeVerifier;
            PontoConnectRedirectUri = pontoConnectRedirectUri;
        }

        public string Endpoint { get; }
        public string MtlsCertificatePath { get; }
        public string MtlsCertificatePassword { get; }
        public string SignatureCertificateId { get; }
        public string SignatureCertificatePath { get; }
        public string SignatureCertificatePassword { get; }
        public string PontoConnectClientId { get; }
        public string PontoConnectClientSecret { get; }
        public string PontoConnectAuthorizationCode { get; }
        public string PontoConnectCodeVerifier { get; }
        public string PontoConnectRedirectUri { get; }

        public static IConfiguration BuildFromEnvironment()
        {
            return new Configuration(
                GetMandatoryEnvironmentVariable("ENDPOINT"),
                GetMandatoryEnvironmentVariable("MTLS_CERTIFICATE_PATH"),
                GetMandatoryEnvironmentVariable("MTLS_CERTIFICATE_PASSWORD"),
                GetMandatoryEnvironmentVariable("SIGNATURE_CERTIFICATE_ID"),
                GetMandatoryEnvironmentVariable("SIGNATURE_CERTIFICATE_PATH"),
                GetMandatoryEnvironmentVariable("SIGNATURE_CERTIFICATE_PASSWORD"),
                GetMandatoryEnvironmentVariable("PONTO_CONNECT_CLIENT_ID"),
                GetMandatoryEnvironmentVariable("PONTO_CONNECT_CLIENT_SECRET"),
                GetMandatoryEnvironmentVariable("PONTO_CONNECT_AUTHORIZATION_CODE"),
                GetMandatoryEnvironmentVariable("PONTO_CONNECT_CODE_VERIFIER"),
                GetMandatoryEnvironmentVariable("PONTO_CONNECT_REDIRECT_URI")
            );
        }

        private static string GetMandatoryEnvironmentVariable(string name)
        {
            var value = Environment.GetEnvironmentVariable(name);

            if (string.IsNullOrWhiteSpace(value))
                throw new ApplicationException($"Missing '{name}' environment variable");

            return value;
        }
    }

    public interface IConfiguration
    {
        public string Endpoint { get; }
        public string MtlsCertificatePath { get; }
        public string MtlsCertificatePassword { get; }
        public string SignatureCertificateId { get; }
        public string SignatureCertificatePath { get; }
        public string SignatureCertificatePassword { get; }
        public string PontoConnectClientId { get; }
        public string PontoConnectClientSecret { get; }
        public string PontoConnectAuthorizationCode { get; }
        public string PontoConnectCodeVerifier { get; }
        public string PontoConnectRedirectUri { get; }
    }
}
