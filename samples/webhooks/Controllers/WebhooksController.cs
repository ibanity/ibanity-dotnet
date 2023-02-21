using Microsoft.AspNetCore.Mvc;

namespace webhooks.Controllers;

[ApiController]
[Route("/")]
public class WebhooksController : ControllerBase
{
    private readonly ILogger<WebhooksController> _logger;

    public WebhooksController(ILogger<WebhooksController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        using var reader = new StreamReader(Request.Body);
        var payload = await reader.ReadToEndAsync();
        return Ok(new { Message = "Webhook received: " + payload.Length });
    }
}
