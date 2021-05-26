using Microsoft.AspNetCore.Mvc;

namespace Uzumachi.McBuilds.Api.Controllers {

  [ApiController]
  [Route("[controller]")]
  public class PostsController : ControllerBase {

    [HttpGet("[action]")]
    public IActionResult Ping() {
      return Ok(new { message = "Pong" });
    }

  }
}
