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
    public IActionResult Post()
    {
        return Ok(new { Message = "Webhook received" });
    }
}
