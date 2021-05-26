using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Uzumachi.McBuilds.Api.Controllers {

  [ApiController]
  [Route("[controller]")]
  public class MainController : ControllerBase {

    private readonly ILogger<MainController> _logger;

    public MainController(ILogger<MainController> logger) {
      _logger = logger;

      _logger.LogDebug("Create HomeController");
    }

    [HttpGet("[action]")]
    public IActionResult Ping() {
      _logger.LogDebug("Ping");

      return Ok(new { message = "Pong" });
    }
  }
}
