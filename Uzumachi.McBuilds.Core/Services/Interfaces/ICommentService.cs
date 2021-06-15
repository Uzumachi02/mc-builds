using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Models;

namespace Uzumachi.McBuilds.Core.Services.Interfaces {

  public interface ICommentService {

    Task<int> CreateForPostAsync(CommentCreateModel comment, CancellationToken token);

    Task<int> UpdateAsync(CommentUpdateModel comment, CancellationToken token);

    Task<int> DeleteAsync(DeleteModel req, CancellationToken token);
  }
}
