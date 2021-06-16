using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Data.Filters;
using Uzumachi.McBuilds.Domain.Entities;

namespace Uzumachi.McBuilds.Data.Repositories.Interfaces {

  public interface ICommentRepository {

    Task<CommentEntity> GetByIdAsync(int id);

    Task<int> GetListCountAsync(CommentFilters filters);

    Task<IEnumerable<CommentEntity>> GetListAsync(CommentFilters filters);

    Task<CommentEntity> GetToRestoreAsync(int id, CancellationToken token = default);

    Task<int> GetTopParentIdByReplyIdAsync(int replyId);

    /// <returns>Id of new item.</returns>
    Task<int> CreateAsync(CommentEntity comment, CancellationToken token, IDbTransaction transaction = null);

    /// <returns>The number of rows affected.</returns>
    Task<int> UpdateAsync(CommentEntity comment, CancellationToken token, IDbTransaction transaction = null);

    /// <returns>The number of rows affected.</returns>
    Task<int> DeleteAsync(CommentEntity comment, CancellationToken token, IDbTransaction transaction = null);

    /// <returns>The number of rows affected.</returns>
    Task<int> RestoreAsync(CommentEntity comment, CancellationToken token, IDbTransaction transaction = null);

    /// <returns>Count replies.</returns>
    Task<int> IncrementRepliesAsync(int commentId, CancellationToken token, IDbTransaction transaction = null);

    /// <returns>Count replies.</returns>
    Task<int> DecrementRepliesAsync(int commentId, CancellationToken token, IDbTransaction transaction = null);
  }
}
