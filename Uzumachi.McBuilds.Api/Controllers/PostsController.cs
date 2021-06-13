using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Mappers;
using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Core.Services.Interfaces;
using Uzumachi.McBuilds.Domain.Dtos;
using Uzumachi.McBuilds.Domain.Requests;
using Uzumachi.McBuilds.Domain.Responses;

namespace Uzumachi.McBuilds.Api.Controllers {

  [ApiController]
  [Route("[controller]")]
  public class PostsController : ControllerBase {

    private readonly IPostsService _postsService;

    public PostsController(IPostsService postsService) =>
      _postsService = postsService;

    [HttpGet]
    public async Task<ActionResult<ItemsResponse<PostDto>>> GetListAsync([FromQuery] PostListRequest req) {
      var posts = await _postsService.GetListAsync(req);

      return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetByIdAsync(int id, [FromQuery] PostGetRequest req) {
      var post = await _postsService.GetByIdAsync(id, req);

      return Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> CreateAsync(PostCreateRequest req, CancellationToken token) {
      var postModel = req.AdaptToPostCreateModel();
      postModel.UserId = 1;

      var newPost = await _postsService.CreateAsync(postModel, token);

      return Ok(newPost);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateAsync(int id, PostUpdateRequest req, CancellationToken token) {
      var postModel = req.AdaptToPostUpdateModel();
      postModel.Id = id;
      postModel.UserId = 1;

      var res = await _postsService.UpdateAsync(postModel, token);

      return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeleteAsync(int id) {
      var req = new DeleteModel { UserId = 1, ItemId = id };

      var res = await _postsService.DeleteAsync(req);

      return Ok(res);
    }

    [HttpPatch("[action]/{id}")]
    public async Task<ActionResult<int>> RestoreAsync(int id) {
      var req = new RestoreModel { UserId = 1, ItemId = id };

      int res = await _postsService.RestoreAsync(req);

      return Ok(res);
    }
  }
}
