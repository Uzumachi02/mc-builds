using System.Collections.Generic;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Data.Entities;

namespace Uzumachi.McBuilds.Data.Repositories.Interfaces {

  public interface IPostRepository {

    Task<IEnumerable<PostEntity>> GetAll();

    ValueTask<PostEntity> GetById(int id);

    ValueTask<int> AddPostAsync(PostEntity newPost);
  }
}
