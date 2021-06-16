using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Mappers;
using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Core.Services.Interfaces;
using Uzumachi.McBuilds.Domain.Dtos;
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

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeleteAsync(int id, CancellationToken token) {
      var req = new DeleteModel { UserId = 1, ItemId = id };
      var res = await _commentService.DeleteAsync(req, token);

      return Ok(res);
    }

    [HttpPatch("[action]/{id}")]
    public async Task<ActionResult<int>> RestoreAsync(int id, CancellationToken token) {
      var req = new RestoreModel { UserId = 1, ItemId = id };
      var res = await _commentService.RestoreAsync(req, token);

      return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetByIdAsync(int id, [FromQuery] PostGetRequest req) {
      var comment = await _commentService.GetByIdAsync(id, req);

      return Ok(comment);
    }
  }
}
