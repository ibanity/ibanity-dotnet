using System.Runtime.CompilerServices;
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

    private Token? _token;

    public PontoConnectModel(ILogger<PontoConnectModel> logger, IIbanityService ibanityService)
    {
        _logger = logger;
        _ibanityService = ibanityService;
    }

    public IActionResult OnGet()
    {
        if (!HttpContext.Session.Keys.Contains("PontoConnectToken"))
            return Redirect("/Products");

        var tokenJson = HttpContext.Session.GetString("PontoConnectToken");
        if (string.IsNullOrWhiteSpace(tokenJson))
            throw new InvalidOperationException("Missing Ponto Connect token");

        _token = JsonSerializer.Deserialize<Token>(tokenJson);

        if (_token == null)
            throw new InvalidOperationException("Null Ponto Connect token");

        _token.RefreshTokenUpdated += (_, _) =>
            HttpContext.Session.SetString("PontoConnectToken", JsonSerializer.Serialize(_token)); ;

        return Page();
    }

    public async IAsyncEnumerable<Account> GetAccounts()
    {
        var page = await _ibanityService.PontoConnect.Accounts.List(_token);

        foreach (var account in page)
            yield return account;

        while (page.ContinuationToken != null)
        {
            page = await _ibanityService.PontoConnect.Accounts.List(_token, page.ContinuationToken);

            foreach (var account in page)
                yield return account;
        }
    }
}
