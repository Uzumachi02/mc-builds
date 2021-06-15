using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Domain.Entities;

namespace Uzumachi.McBuilds.Data.Repositories.Interfaces {

  public interface ICommentRepository {

    Task<CommentEntity> GetByIdAsync(int id);

    Task<int> GetTopParentIdByReplyIdAsync(int replyId);

    Task<int> CreateAsync(CommentEntity comment, CancellationToken token, IDbTransaction transaction = null);

    Task<int> IncrementRepliesAsync(int commentId, CancellationToken token, IDbTransaction transaction = null);
  }
}
