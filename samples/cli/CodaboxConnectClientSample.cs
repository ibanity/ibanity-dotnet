using Ibanity.Apis.Client;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.CodaboxConnect;
using Ibanity.Apis.Client.Products.CodaboxConnect.Models;

namespace Ibanity.Apis.Sample.CLI
{
    public class CodaboxConnectClientSample
    {
        private readonly IIbanityService _ibanityService;

        public CodaboxConnectClientSample(IIbanityService ibanityService)
        {
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

            var creditCardStatements = await GetDocuments(codaboxConnectService, token, consent, "creditCardStatement", cancellationToken);
            var payrollStatements = await GetDocuments(codaboxConnectService, token, consent, "payrollStatement", cancellationToken);
            var documents = creditCardStatements.Union(payrollStatements);

            foreach (var document in documents)
                switch (document)
                {
                    case CreditCardStatement doc:
                        Console.WriteLine($"Document: {doc.Type} {doc.Id} for {doc.Client} from {doc.BankName}");
                        break;
                    default:
                        Console.WriteLine($"Document: {document.Type} {document.Id} for {document.Client}");
                        break;
                }

        }

        private static async Task<IDocument[]> GetDocuments(ICodaboxConnectClient service, ClientAccessToken token, AccountingOfficeConsentResponse consent, string documentType, CancellationToken cancellationToken)
        {
            var documentSearch = new DocumentSearch
            {
                From = new DateTimeOffset(2019, 9, 26, 7, 58, 30, TimeSpan.Zero),
                To = new DateTimeOffset(2020, 1, 1, 0, 0, 0, 996, TimeSpan.Zero),
                DocumentType = documentType
            };

            var search = await service.DocumentSearches.Create(
                token,
                consent.AccountingOfficeId,
                documentSearch,
                new[] { consent.AccountingOfficeCompanyNumber },
                cancellationToken: cancellationToken);

            return search.Documents;
        }
    }
}
