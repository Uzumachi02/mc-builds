using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Core.Services.Interfaces;
using Uzumachi.McBuilds.Data.Interfaces;

namespace Uzumachi.McBuilds.Api.Controllers {

  [ApiController]
  [Route("[controller]")]
  public class PostsController : ControllerBase {

    private readonly IUnitOfWork _unitOfWork;
    private readonly IPostsService _postsService;

    public PostsController(IUnitOfWork unitOfWork, IPostsService postsService) {
      _unitOfWork = unitOfWork;
      _postsService = postsService;
    }

    [HttpGet("[action]")]
    public IActionResult Ping() {
      return Ok(new { message = "Pong" });
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll() {

      var users = await _unitOfWork.Posts.GetAll();

      return Ok(users);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<PostModel>> CreateAsync(CreatePostModel post) {
      post.UserId = 1;
      var newPost = await _postsService.CreateAsync(post);

      return Ok(newPost);
    }
  }
}
