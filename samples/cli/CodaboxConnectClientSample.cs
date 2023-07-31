using Ibanity.Apis.Client;
using Ibanity.Apis.Client.Products.CodaboxConnect.Models;

namespace Ibanity.Apis.Sample.CLI
{
    public class CodaboxConnectClientSample
    {
        private readonly IConfiguration _configuration;
        private readonly IIbanityService _ibanityService;

        public CodaboxConnectClientSample(IConfiguration configuration, IIbanityService ibanityService)
        {
            _configuration = configuration;
            _ibanityService = ibanityService;
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            Console.WriteLine("Running Codabox Connect client sample...");

            var codaboxConnectService = _ibanityService.CodaboxConnect;

            var token = await codaboxConnectService.ClientTokenService.GetToken(cancellationToken);

            var consent = await codaboxConnectService.AccountingOfficeConsents.Create(
                token,
                new NewAccountingOfficeConsent
                {
                    AccountingOfficeCompanyNumber = "0123456789",
                    RedirectUri = "https://mydomain.localhost/callback"
                },
                cancellationToken);

            var documentSearch = new DocumentSearch
            {
                From = new DateTimeOffset(2019, 9, 26, 7, 58, 30, TimeSpan.Zero),
                To = new DateTimeOffset(2020, 1, 1, 0, 0, 0, 996, TimeSpan.Zero),
                DocumentType = "creditCardStatement"
            };

            var search = await codaboxConnectService.DocumentSearches.Create(
                token,
                consent.AccountingOfficeId,
                documentSearch,
                new[] { consent.AccountingOfficeCompanyNumber },
                cancellationToken: cancellationToken);

            foreach (var document in search.Documents)
                Console.WriteLine("Document: " + document);
        }
    }
}
