using Ibanity.Apis.Client;
using Ibanity.Apis.Client.Webhooks.Models.PontoConnect;
using Microsoft.AspNetCore.Mvc;

namespace webhooks.Controllers;

[ApiController]
[Route("/")]
public class WebhooksController : ControllerBase
{
    private readonly ILogger<WebhooksController> _logger;
    private readonly IIbanityService _ibanityService;

    public WebhooksController(ILogger<WebhooksController> logger, IIbanityService ibanityService)
    {
        _logger = logger;
        _ibanityService = ibanityService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromHeader] string signature)
    {
        string payload;
        using (var reader = new StreamReader(Request.Body))
            payload = await reader.ReadToEndAsync();

        var content = await _ibanityService.Webhooks.VerifyAndDeserialize(payload, signature);

        switch (content)
        {
            case IntegrationAccountAdded integrationAccountAdded:
                _logger.LogInformation($"Integration account added webhook received: {content.Id} (account {integrationAccountAdded.AccountId})");
                break;
            default:
                _logger.LogInformation($"Webhook received: {content.Id} ({content.GetType().Name})");
                break;
        }

        return Ok(new { Message = "Webhook received: " + payload.Length });
    }
}
