using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Data.Filters;
using Uzumachi.McBuilds.Domain.Entities;

namespace Uzumachi.McBuilds.Data.Repositories.Interfaces {

  public interface IPostRepository {

    Task<IEnumerable<PostEntity>> GetAll();

    Task<IEnumerable<PostEntity>> GetListAsync(PostFilters filters);

    Task<int> GetListCountAsync(PostFilters filters);

    ValueTask<PostEntity> GetById(int id);

    ValueTask<int> AddPostAsync(PostEntity newPost, CancellationToken token, IDbTransaction transaction = null);

    ValueTask<int> IncrementLikeForPost(int postID);

    ValueTask<int> DecrementLikeForPost(int postID);

    Task<PostEntity> GetByIdForUser(int id, int userId);

    Task<int> DeleteAsync(int id);
  }
}
