using System.Text.Json;
using Ibanity.Apis.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webapp.Pages;

public class CallbackModel : PageModel
{
    private readonly ILogger<CallbackModel> _logger;
    private readonly IConfiguration _configuration;
    private readonly IIbanityService _ibanityService;

    public CallbackModel(ILogger<CallbackModel> logger, IConfiguration configuration, IIbanityService ibanityService)
    {
        _logger = logger;
        _configuration = configuration;
        _ibanityService = ibanityService;
    }

    public async Task<IActionResult> OnGet(CancellationToken cancellationToken)
    {
        var knownState = HttpContext.Session.GetString("PontoConnectAuthenticationState");
        if (string.IsNullOrWhiteSpace(knownState))
        {
            Error = "Missing known state";
            return Page();
        }

        HttpContext.Session.Remove("PontoConnectAuthenticationState");

        var knownPkceCode = HttpContext.Session.GetString("PontoConnectAuthenticationPkceCode");
        if (string.IsNullOrWhiteSpace(knownPkceCode))
        {
            Error = "Missing known PKCE code";
            return Page();
        }

        HttpContext.Session.Remove("PontoConnectAuthenticationPkceCode");

        var error = HttpContext.Request.Query["error"].FirstOrDefault();

        if (!string.IsNullOrWhiteSpace(error))
        {
            Error = error;
            return Page();
        }

        var state = HttpContext.Request.Query["state"].SingleOrDefault();
        if (string.IsNullOrWhiteSpace(state))
        {
            Error = "Missing state";
            return Page();
        }

        if (state != knownState)
        {
            Error = "State mismatch";
            return Page();
        }

        var code = HttpContext.Request.Query["code"].SingleOrDefault();
        if (string.IsNullOrWhiteSpace(code))
        {
            Error = "Missing authorization code";
            return Page();
        }

        var token = await _ibanityService.PontoConnect.TokenService.GetToken(
            code,
            knownPkceCode,
            $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/callback",
            cancellationToken);

        HttpContext.Session.SetString("PontoConnectToken", JsonSerializer.Serialize(token));

        return Redirect("Products");
    }

    public string? Error { get; private set; }
}
