using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Core.Services.Interfaces;

namespace Uzumachi.McBuilds.Api.Controllers {

  [ApiController]
  [Route("[controller]")]
  public class LikesController : ControllerBase {

    private readonly ILikesService _likesService;

    public LikesController(ILikesService likesService) =>
      (_likesService) = (likesService);

    [HttpGet("[action]")]
    public IActionResult Ping() {
      return Ok(new { message = "Pong" });
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<LikeResponseModel>> PostAsync(int postId) {
      var req = new LikeRequestModel { UserId = 1, ItemId = postId };

      var res = await _likesService.LikePostAsync(req);

      return Ok(res);
    }

    [HttpDelete("Post")]
    public async Task<ActionResult<LikeResponseModel>> PostDelete(int postId) {
      var req = new LikeRequestModel { UserId = 1, ItemId = postId };

      var res = await _likesService.DeleteLikePostAsync(req);

      return Ok(res);
    }
  }
}
