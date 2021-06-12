using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Mappers;
using Uzumachi.McBuilds.Core.Services.Interfaces;
using Uzumachi.McBuilds.Data.Interfaces;
using Uzumachi.McBuilds.Domain.Dtos;
using Uzumachi.McBuilds.Domain.Requests;

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
    public async Task<ActionResult<IEnumerable<PostDto>>> GetAll() {
      var dbPosts = await _unitOfWork.Posts.GetAll();
      var posts = dbPosts.Select(x => x.AdaptToPostDto()).ToArray();

      foreach( var post in posts ) {
        var postAttachments = await _unitOfWork.PostAttachments.GetListForPost(post.Id);
        post.Attachments = postAttachments.Select(a => a.AdaptToPostAttachmentDto()).ToArray();
      }

      return Ok(posts);
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
