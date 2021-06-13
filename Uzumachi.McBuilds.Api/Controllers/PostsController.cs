using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Mappers;
using Uzumachi.McBuilds.Core.Services.Interfaces;
using Uzumachi.McBuilds.Domain.Dtos;
using Uzumachi.McBuilds.Domain.Requests;

namespace Uzumachi.McBuilds.Api.Controllers {

  [ApiController]
  [Route("[controller]")]
  public class PostsController : ControllerBase {
    private readonly IPostsService _postsService;

    public PostsController(IPostsService postsService) =>
      _postsService = postsService;

    [HttpGet("/")]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetListAsync([FromQuery] PostListRequest req) {
      var posts = await _postsService.GetListAsync(req);

      return Ok(posts);
    }

    [HttpGet("/{id}")]
    public async Task<ActionResult<PostDto>> GetByIdAsync(int id, [FromQuery] PostGetRequest req) {
      var post = await _postsService.GetByIdAsync(id, req);

      return Ok(post);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<PostDto>> CreateAsync(PostCreateRequest req, CancellationToken token) {
      var postModel = req.AdaptToPostCreateModel();
      postModel.UserId = 1;

      var newPost = await _postsService.CreateAsync(postModel, token);

      return Ok(newPost);
    }
  }
}
