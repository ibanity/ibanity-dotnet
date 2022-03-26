using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using Ibanity.Apis.Client.Crypto;
using Ibanity.Apis.Client.Products.PontoConnect;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Http
{
    public class IbanityServiceBuilder : IIbanityServiceBuilder
    {
        public IIbanityService Build(
            Uri endpoint,
            IWebProxy proxy,
            X509Certificate2 clientCertificate,
            X509Certificate2 signatureCertificate,
            string signatureCertificateId,
            string clientId,
            string clientSecret,
            string authorizationCode,
            string codeVerifier,
            Uri redirectUri,
            string refreshToken)
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

            var signatureService = signatureCertificate == null
                ? NullHttpSignatureService.Instance
                : new HttpSignatureService(
                    new Sha512Digest(),
                    new RsaSsaPssSignature(signatureCertificate),
                    clock,
                    new HttpSignatureString(endpoint),
                    signatureCertificateId);

            var apiClient = new ApiClient(httpClient, serializer, signatureService);

            var tokenProvider = refreshToken == null
                ? new OAuth2TokenProvider(httpClient, clock, clientId, clientSecret, authorizationCode, codeVerifier, redirectUri)
                : new OAuth2TokenProvider(httpClient, clock, refreshToken);

            var pontoConnectClient = new PontoConnectClient(
                apiClient,
                tokenProvider,
                new OAuth2ClientAccessTokenProvider(httpClient, clock, clientId, clientSecret));

            return new IbanityService(httpClient, pontoConnectClient);
        }
    }

    public interface IIbanityServiceBuilder
    {
        IIbanityService Build(
            Uri endpoint,
            IWebProxy proxy,
            X509Certificate2 clientCertificate,
            X509Certificate2 signatureCertificate,
            string signatureCertificateId,
            string clientId,
            string clientSecret,
            string authorizationCode,
            string codeVerifier,
            Uri redirectUri,
            string refreshToken);
    }
}
