using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Data.Entities;

namespace Uzumachi.McBuilds.Data.Repositories.Interfaces {

  public interface IPostAttachmentRepository {

    Task<int> AddAsync(IEnumerable<PostAttachmentEntity> postAttachments, CancellationToken token, IDbTransaction transaction = null);

    Task<IEnumerable<PostAttachmentEntity>> GetListForPost(int postId, int limit = 10);
  }
}
