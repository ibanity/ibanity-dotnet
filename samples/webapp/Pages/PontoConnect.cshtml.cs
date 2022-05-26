using System.Text.Json;
using Ibanity.Apis.Client;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.PontoConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webapp.Pages;

public class PontoConnectModel : PageModel
{
    private readonly ILogger<PontoConnectModel> _logger;
    private readonly IIbanityService _ibanityService;

    public PontoConnectModel(ILogger<PontoConnectModel> logger, IIbanityService ibanityService)
    {
        _logger = logger;
        _ibanityService = ibanityService;
    }

    public IActionResult OnGet()
    {
        if (!HttpContext.Session.Keys.Contains("PontoConnectToken"))
            return Redirect("/Products");

        return Page();
    }

    public async IAsyncEnumerable<Account> GetAccounts()
    {
        var tokenJson = HttpContext.Session.GetString("PontoConnectToken");
        if (string.IsNullOrWhiteSpace(tokenJson))
            throw new InvalidOperationException("Missing Ponto Connect token");

        var token = JsonSerializer.Deserialize<Token>(tokenJson);

        if (token == null)
            throw new InvalidOperationException("Null Ponto Connect token");

        token.RefreshTokenUpdated += (_, _) =>
            HttpContext.Session.SetString("PontoConnectToken", JsonSerializer.Serialize(token));

        var page = await _ibanityService.PontoConnect.Accounts.List(token);

        foreach (var account in page.Items)
            yield return account;

        while (page.ContinuationToken != null)
        {
            page = await _ibanityService.PontoConnect.Accounts.List(token, page.ContinuationToken);

            foreach (var account in page.Items)
                yield return account;
        }
    }
}
