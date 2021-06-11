using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Uzumachi.McBuilds.Api.Requests;
using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Core.Services.Interfaces;
using Uzumachi.McBuilds.Data.Interfaces;
using System.Collections.Generic;

namespace Uzumachi.McBuilds.Api.Controllers {

  [ApiController]
  [Route("[controller]")]
  public class PostsController : ControllerBase {

    private readonly IUnitOfWork _unitOfWork;
    private readonly IPostsService _postsService;
    private readonly IMapper _mapper;

    public PostsController(IUnitOfWork unitOfWork, IPostsService postsService, IMapper mapper) {
      _unitOfWork = unitOfWork;
      _postsService = postsService;
      _mapper = mapper;
    }

    [HttpGet("[action]")]
    public IActionResult Ping() {
      return Ok(new { message = "Pong" });
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll() {

      var dbPosts = await _unitOfWork.Posts.GetAll();
      var posts = _mapper.Map<List<PostModel>>(dbPosts);

      foreach( var post in posts ) {
        var postAttachments = await _unitOfWork.PostAttachments.GetListForPost(post.Id);
        post.Attachments = _mapper.Map<List<AttachmentModel>>(postAttachments);
      }

      return Ok(posts);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<PostModel>> CreateAsync(CreatePostRequest req, CancellationToken token) {
      var postModel = new CreatePostModel {
        UserId = 1,
        Text = req.Text,
        CloseComments = req.CloseComments,
        Attachments = req.Attachments
      };

      var newPost = await _postsService.CreateAsync(postModel, token);

      return Ok(newPost);
    }
  }
}
