using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Mappers;
using Uzumachi.McBuilds.Core.Services.Interfaces;
using Uzumachi.McBuilds.Domain.Requests;

namespace Uzumachi.McBuilds.Api.Controllers {

  [ApiController]
  [Route("[controller]")]
  public class CommentsController : ControllerBase {

    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService) =>
      _commentService = commentService;

    [HttpPost("[action]")]
    public async Task<ActionResult<int>> PostAsync(CommentCreateRequest req, CancellationToken token) {
      var commentModel = req.AdaptToCommentCreateModel();
      commentModel.UserId = 1;

      var res = await _commentService.CreateForPostAsync(commentModel, token);

      return Ok(res);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateAsync(int id, CommentUpdateRequest req, CancellationToken token) {
      var commentModel = req.AdaptToCommentUpdateModel();
      commentModel.Id = id;
      commentModel.UserId = 1;

      var res = await _commentService.UpdateAsync(commentModel, token);

      return Ok(res);
    }
  }
}
