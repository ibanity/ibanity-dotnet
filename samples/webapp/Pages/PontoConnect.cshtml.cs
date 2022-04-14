using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webapp.Pages;

public class PontoConnectModel : PageModel
{
    private readonly ILogger<PontoConnectModel> _logger;

    public PontoConnectModel(ILogger<PontoConnectModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        if (!HttpContext.Session.Keys.Contains("PontoConnectToken"))
            return Redirect("/Products");

        return Page();
    }
}
