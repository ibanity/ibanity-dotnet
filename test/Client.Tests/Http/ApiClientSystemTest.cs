using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ibanity.Apis.Client.Tests.Http
{
    [TestClass]
    public class ApiClientSystemTest
    {
        [TestMethod]
        public async Task AbleToAuthenticateAndGetFinancialInstitutions()
        {
            var certificatePath = Environment.GetEnvironmentVariable("MTLS_CERTIFICATE_PATH");
            var certificatePassword = Environment.GetEnvironmentVariable("MTLS_CERTIFICATE_PASSWORD");

            if (string.IsNullOrWhiteSpace(certificatePath) || string.IsNullOrWhiteSpace(certificatePassword))
                Assert.Inconclusive("Missing 'MTLS_CERTIFICATE_PATH' and 'MTLS_CERTIFICATE_PASSWORD' environment variables");

            var nullSerializer = new Mock<ISerializer<string>>();
            nullSerializer.
                Setup(s => s.Deserialize<string>(It.IsAny<string>())).
                Returns<string>(v => v);

            var certificate = new X509Certificate2(certificatePath!, certificatePassword);

            var target = new ApiClient(
                nullSerializer.Object,
                new Uri("https://api.ibanity.com"),
                certificate,
                null);

            var result = await target.Get<string>("ponto-connect/financial-institutions", CancellationToken.None);

            Assert.IsNotNull(result);
        }
    }
}
