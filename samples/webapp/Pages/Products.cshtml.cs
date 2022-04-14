using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webapp.Pages;

public class ProductsModel : PageModel
{
    private readonly ILogger<ProductsModel> _logger;
    private readonly IConfiguration _configuration;

    public ProductsModel(ILogger<ProductsModel> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public string PontoConnectAuthorizationUri =>
        $"{_configuration["Ibanity:PontoConnect:AuthenticationUri"]}?client_id={_configuration["Ibanity:PontoConnect:ClientId"]}&redirect_uri={HttpUtility.UrlEncode($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/callback")}&response_type=code&scope={HttpUtility.UrlEncode(_configuration["Ibanity:PontoConnect:Scope"])}&state={GenerateState()}&code_challenge={GenerateCodeChallenge()}&code_challenge_method=S256";

    private string GenerateNonce(int size)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var nonce = new char[size];
        for (int i = 0; i < nonce.Length; i++)
        {
            nonce[i] = chars[RandomNumberGenerator.GetInt32(chars.Length)];
        }

        return new string(nonce);
    }

    private string GenerateState()
    {
        var state = GenerateNonce(32);

        HttpContext.Session.SetString("PontoConnectAuthenticationState", state);

        return state;
    }

    private string GenerateCodeChallenge()
    {
        var codeVerifier = GenerateNonce(64);

        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
        var b64Hash = Convert.ToBase64String(hash);
        var code = Regex.Replace(b64Hash, "\\+", "-");
        code = Regex.Replace(code, "\\/", "_");
        code = Regex.Replace(code, "=+$", "");

        HttpContext.Session.SetString("PontoConnectAuthenticationPkceCode", codeVerifier);

        return code;
    }

    public void OnGet()
    {
    }
}
