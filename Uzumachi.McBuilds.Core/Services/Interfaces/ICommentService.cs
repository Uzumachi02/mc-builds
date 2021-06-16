using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Domain.Dtos;
using Uzumachi.McBuilds.Domain.Requests;
using Uzumachi.McBuilds.Domain.Responses;

namespace Uzumachi.McBuilds.Core.Services.Interfaces {

  public interface ICommentService {

    Task<int> CreateForPostAsync(CommentCreateModel comment, CancellationToken token);

    Task<int> UpdateAsync(CommentUpdateModel comment, CancellationToken token);

    Task<int> DeleteAsync(DeleteModel req, CancellationToken token);

    Task<int> RestoreAsync(RestoreModel req, CancellationToken token);

    Task<CommentDto> GetByIdAsync(int id, PostGetRequest req);

    Task<ItemsResponse<CommentDto>> GetListAsync(CommentListRequest req);
  }
}
