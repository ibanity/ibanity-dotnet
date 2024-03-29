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
            string? pontoConnectAuthorizationCode,
            string pontoConnectCodeVerifier,
            string pontoConnectRedirectUri,
            string? pontoConnectRefreshToken,
            string isabelConnectClientId,
            string isabelConnectClientSecret,
            string? isabelConnectAuthorizationCode,
            string isabelConnectRedirectUri,
            string codaboxConnectClientId,
            string codaboxConnectClientSecret)
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
            PontoConnectRefreshToken = pontoConnectRefreshToken;
            IsabelConnectClientId = isabelConnectClientId;
            IsabelConnectClientSecret = isabelConnectClientSecret;
            IsabelConnectAuthorizationCode = isabelConnectAuthorizationCode;
            IsabelConnectRedirectUri = isabelConnectRedirectUri;
            CodaboxConnectClientId = codaboxConnectClientId;
            CodaboxConnectClientSecret = codaboxConnectClientSecret;
        }

        public string Endpoint { get; }
        public string MtlsCertificatePath { get; }
        public string MtlsCertificatePassword { get; }
        public string SignatureCertificateId { get; }
        public string SignatureCertificatePath { get; }
        public string SignatureCertificatePassword { get; }
        public string PontoConnectClientId { get; }
        public string PontoConnectClientSecret { get; }
        public string? PontoConnectAuthorizationCode { get; }
        public string PontoConnectCodeVerifier { get; }
        public string PontoConnectRedirectUri { get; }
        public string? PontoConnectRefreshToken { get; }
        public string IsabelConnectClientId { get; }
        public string IsabelConnectClientSecret { get; }
        public string? IsabelConnectAuthorizationCode { get; }
        public string IsabelConnectRedirectUri { get; }
        public string CodaboxConnectClientId { get; }
        public string CodaboxConnectClientSecret { get; }

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
                Environment.GetEnvironmentVariable("PONTO_CONNECT_AUTHORIZATION_CODE"),
                GetMandatoryEnvironmentVariable("PONTO_CONNECT_CODE_VERIFIER"),
                GetMandatoryEnvironmentVariable("PONTO_CONNECT_REDIRECT_URI"),
                Environment.GetEnvironmentVariable("PONTO_CONNECT_REFRESH_TOKEN"),
                GetMandatoryEnvironmentVariable("ISABEL_CONNECT_CLIENT_ID"),
                GetMandatoryEnvironmentVariable("ISABEL_CONNECT_CLIENT_SECRET"),
                Environment.GetEnvironmentVariable("ISABEL_CONNECT_AUTHORIZATION_CODE"),
                GetMandatoryEnvironmentVariable("ISABEL_CONNECT_REDIRECT_URI"),
                GetMandatoryEnvironmentVariable("CODABOX_CONNECT_CLIENT_ID"),
                GetMandatoryEnvironmentVariable("CODABOX_CONNECT_CLIENT_SECRET")
            );
        }

        private static string GetMandatoryEnvironmentVariable(string name)
        {
            var value = Environment.GetEnvironmentVariable(name);

            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidOperationException($"Missing '{name}' environment variable");

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
        public string? PontoConnectAuthorizationCode { get; }
        public string PontoConnectCodeVerifier { get; }
        public string PontoConnectRedirectUri { get; }
        public string? PontoConnectRefreshToken { get; }
        public string IsabelConnectClientId { get; }
        public string IsabelConnectClientSecret { get; }
        public string? IsabelConnectAuthorizationCode { get; }
        public string IsabelConnectRedirectUri { get; }
        public string CodaboxConnectClientId { get; }
        public string CodaboxConnectClientSecret { get; }
    }
}
