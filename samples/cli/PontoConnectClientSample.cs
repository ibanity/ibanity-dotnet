using System.Globalization;
using Ibanity.Apis.Client;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Sample.CLI
{
    public class PontoConnectClientSample
    {
        private readonly IConfiguration _configuration;
        private readonly IIbanityService _ibanityService;

        public PontoConnectClientSample(IConfiguration configuration, IIbanityService ibanityService)
        {
            _configuration = configuration;
            _ibanityService = ibanityService;
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            Console.WriteLine("Running Ponto Connect client sample...");

            var pontoConnectService = _ibanityService.PontoConnect;

            var token = string.IsNullOrWhiteSpace(_configuration.PontoConnectRefreshToken)
                ? await pontoConnectService.TokenService.GetToken(
                    _configuration.PontoConnectAuthorizationCode ?? throw new InvalidOperationException("Either authorization code or refresh token must be set"),
                    _configuration.PontoConnectCodeVerifier,
                    _configuration.PontoConnectRedirectUri,
                    cancellationToken)
                : await pontoConnectService.TokenService.GetToken(
                    _configuration.PontoConnectRefreshToken,
                    cancellationToken);

            Console.Error.WriteLine("Ponto Connect refresh token: " + token.RefreshToken);
            token.RefreshTokenUpdated += (_, e) => Console.Error.WriteLine("Ponto Connect refresh token updated: " + e.NewToken);

            var financialInstitutions = await pontoConnectService.FinancialInstitutions.ListForOrganization(token, cancellationToken: cancellationToken);
            foreach (var financialInstitution in financialInstitutions.Items)
                Console.WriteLine("Financial institution: " + financialInstitution);

            while (financialInstitutions.ContinuationToken != null)
            {
                financialInstitutions = await pontoConnectService.FinancialInstitutions.ListForOrganization(token, financialInstitutions.ContinuationToken, cancellationToken);
                foreach (var financialInstitution in financialInstitutions.Items)
                    Console.WriteLine("Financial institution: " + financialInstitution);
            }

            var userInfo = await pontoConnectService.UserInfo.Get(token, cancellationToken);
            Console.WriteLine("User information: " + userInfo);

            var sanboxService = pontoConnectService.Sandbox;

            var sandboxAccounts = await sanboxService.Accounts.List(token, financialInstitutions.Items.First().Id, cancellationToken: cancellationToken);
            foreach (var sandboxAccount in sandboxAccounts.Items)
                Console.WriteLine("Sandbox account: " + sandboxAccount);

            var sandboxTransaction = await sanboxService.Transactions.Create(token, financialInstitutions.Items.First().Id, sandboxAccounts.Items.First().Id, new Transaction
            {
                RemittanceInformationType = "unstructured",
                RemittanceInformation = "NEW SHOES",
                Description = "Small Cotton Shoes",
                Currency = "EUR",
                CounterpartName = "Otro Bank",
                PurposeCode = "DEBIT",
                CounterpartReference = "BE9786154282554",
                Amount = 84.42m,
                ValueDate = DateTimeOffset.Parse("2020-05-22T00:00:00Z", CultureInfo.InvariantCulture),
                ExecutionDate = DateTimeOffset.Parse("2020-05-25T00:00:00Z", CultureInfo.InvariantCulture)
            }, cancellationToken: cancellationToken);

            Console.WriteLine("Sandbox transaction created: " + sandboxTransaction);

            sandboxTransaction = await sanboxService.Transactions.Update(token, financialInstitutions.Items.First().Id, sandboxAccounts.Items.First().Id, sandboxTransaction.Id, new Transaction
            {
                RemittanceInformation = "NEW SHOES",
                Description = "Hole foods",
                BankTransactionCode = "PMNT-IRCT-ESCT",
                ProprietaryBankTransactionCode = "12267",
                AdditionalInformation = "Online payment on fake-tpp.com",
                CreditorId = "123498765421",
                MandateId = "234",
                PurposeCode = "CASH",
                EndToEndId = "ref.243435343"
            }, cancellationToken: cancellationToken);

            Console.WriteLine("Sandbox transaction updated: " + sandboxTransaction);

            sandboxTransaction = await sanboxService.Transactions.Get(token, financialInstitutions.Items.First().Id, sandboxAccounts.Items.First().Id, sandboxTransaction.Id, cancellationToken);

            Console.WriteLine("Sandbox transaction: " + sandboxTransaction);

            var sandboxTransactions = await sanboxService.Transactions.List(token, financialInstitutions.Items.First().Id, sandboxAccounts.Items.First().Id, cancellationToken: cancellationToken);
            foreach (var tr in sandboxTransactions.Items)
                Console.WriteLine("Sandbox transaction: " + tr);

            var accounts = await pontoConnectService.Accounts.List(token, cancellationToken: cancellationToken);
            foreach (var account in accounts.Items)
                Console.WriteLine("Account: " + account);

            var accountId = accounts.Items.First().Id;

            var synchronization = await pontoConnectService.Synchronizations.Create(token, new SynchronizationRequest
            {
                ResourceType = "account",
                Subtype = "accountDetails",
                ResourceId = accountId,
                CustomerIpAddress = "123.123.123.123"
            }, cancellationToken: cancellationToken);

            Console.WriteLine("Synchronization created: " + synchronization);

            synchronization = await pontoConnectService.Synchronizations.Get(token, synchronization.Id, cancellationToken);

            Console.WriteLine("Synchronization: " + synchronization);

            var updatedTransactions = await pontoConnectService.Transactions.ListUpdatedForSynchronization(token, synchronization.Id, cancellationToken: cancellationToken);

            var transactions = await pontoConnectService.Transactions.List(token, accountId, cancellationToken: cancellationToken);
            foreach (var tr in transactions.Items)
                Console.WriteLine("Transaction: " + tr);

            var transaction = await pontoConnectService.Transactions.Get(token, accountId, transactions.Items.First().Id, cancellationToken);
            Console.WriteLine("Transaction: " + transaction);

            var payment = await pontoConnectService.Payments.Create(token, accountId, new PaymentRequest
            {
                RemittanceInformation = "payment",
                RemittanceInformationType = "unstructured",
                RequestedExecutionDate = DateTimeOffset.Now.AddDays(1d),
                Currency = "EUR",
                Amount = 59m,
                CreditorName = "Alex Creditor",
                CreditorAccountReference = "BE55732022998044",
                CreditorAccountReferenceType = "IBAN",
                CreditorAgent = "NBBEBEBB203",
                CreditorAgentType = "BIC"
            }, cancellationToken: cancellationToken);

            Console.WriteLine("Payment created: " + payment);

            payment = await pontoConnectService.Payments.Get(token, accountId, payment.Id, cancellationToken);

            Console.WriteLine("Payment: " + payment);

            await pontoConnectService.Payments.Delete(token, accountId, payment.Id, cancellationToken);

            Console.WriteLine($"Payment {payment.Id} deleted");

            var bulkPayment = await pontoConnectService.BulkPayments.Create(token, accountId, new BulkPaymentRequest
            {
                BatchBookingPreferred = true,
                Reference = "myReference",
                RequestedExecutionDate = DateTimeOffset.Now.AddDays(1d),
                RedirectUri = "",
                Payments = new List<Payment> { new Payment
                {
                    RemittanceInformation = "payment",
                    RemittanceInformationType = "unstructured",
                    Currency = "EUR",
                    Amount = 59m,
                    CreditorName = "Alex Creditor",
                    CreditorAccountReference = "BE55732022998044",
                    CreditorAccountReferenceType = "IBAN",
                    CreditorAgent = "NBBEBEBB203",
                    CreditorAgentType = "BIC"
                } }
            }, cancellationToken: cancellationToken);

            Console.WriteLine("Bulk payment created: " + bulkPayment);

            bulkPayment = await pontoConnectService.BulkPayments.Get(token, accountId, bulkPayment.Id, cancellationToken);

            Console.WriteLine("Bulk payment: " + bulkPayment);

            await pontoConnectService.BulkPayments.Delete(token, accountId, bulkPayment.Id, cancellationToken);

            Console.WriteLine($"Bulk payment {bulkPayment.Id} deleted");
        }
    }
}
