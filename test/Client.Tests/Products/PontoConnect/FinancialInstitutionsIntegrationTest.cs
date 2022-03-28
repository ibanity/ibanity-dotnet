using System;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Models;
using Ibanity.Apis.Client.Utils.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ibanity.Apis.Client.Tests.Http
{
    [TestClass]
    public class FinancialInstitutionsIntegrationTest
    {
        [TestMethod]
        public async Task AbleToAuthenticateAndGetFinancialInstitutions()
        {
            var certificatePath = Environment.GetEnvironmentVariable("MTLS_CERTIFICATE_PATH");
            var certificatePassword = Environment.GetEnvironmentVariable("MTLS_CERTIFICATE_PASSWORD");

            if (string.IsNullOrWhiteSpace(certificatePath) || string.IsNullOrWhiteSpace(certificatePassword))
                Assert.Inconclusive("Missing 'MTLS_CERTIFICATE_PATH' or 'MTLS_CERTIFICATE_PASSWORD' environment variables");

            var signatureCertificatePath = Environment.GetEnvironmentVariable("SIGNATURE_CERTIFICATE_PATH");
            var signatureCertificatePassword = Environment.GetEnvironmentVariable("SIGNATURE_CERTIFICATE_PASSWORD");

            if (string.IsNullOrWhiteSpace(signatureCertificatePath) || string.IsNullOrWhiteSpace(signatureCertificatePassword))
                Assert.Inconclusive("Missing 'SIGNATURE_CERTIFICATE_PATH' or 'SIGNATURE_CERTIFICATE_PASSWORD' environment variables");

            var pontoConnectClientId = Environment.GetEnvironmentVariable("PONTO_CONNECT_CLIENT_ID");
            var pontoConnectClientSecret = Environment.GetEnvironmentVariable("PONTO_CONNECT_CLIENT_SECRET");

            if (string.IsNullOrWhiteSpace(pontoConnectClientId) || string.IsNullOrWhiteSpace(pontoConnectClientSecret))
                Assert.Inconclusive("Missing 'PONTO_CONNECT_CLIENT_ID' or 'PONTO_CONNECT_CLIENT_SECRET' environment variables");

            var expectedDebugLog = "Sending request: GET ponto-connect/financial-institutions/433329cb-3a66-4d47-8387-98bdaa5e55ad";

            var logger = new Mock<ILogger>();
            logger.
                Setup(l => l.Debug(expectedDebugLog));

            var ibanityService = new IbanityServiceBuilder().
                SetEndpoint("https://api.ibanity.com").
                AddClientCertificate(certificatePath, certificatePassword).
                AddSignatureCertificate("7705c535-e9b4-416d-9a4a-97337b24fa1b", signatureCertificatePath, signatureCertificatePassword).
                AddPontoConnectOAuth2Authentication(pontoConnectClientId, pontoConnectClientSecret).
                AddLogging(logger.Object).
                Build();

            Guid id = Guid.Parse("433329cb-3a66-4d47-8387-98bdaa5e55ad");
            var financialInstitution = await ibanityService.PontoConnect.FinancialInstitutions.Get(id);
            Assert.IsNotNull(financialInstitution);

            logger.
                Verify(l => l.Debug(expectedDebugLog), Times.Once);

            var filters = new[] { new Filter("name", FilterOperator.Like, "gring") };
            var financialInstitutions = await ibanityService.PontoConnect.FinancialInstitutions.List(filters);
            Assert.IsNotNull(financialInstitutions);
        }
    }
}
