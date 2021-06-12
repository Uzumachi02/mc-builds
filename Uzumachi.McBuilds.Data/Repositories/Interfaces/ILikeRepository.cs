using System.Threading.Tasks;
using Uzumachi.McBuilds.Domain.Entities;

namespace Uzumachi.McBuilds.Data.Repositories.Interfaces {

  public interface ILikeRepository {

    ValueTask<int> CreateAsync(LikeEntity newLike);

    /// <returns>The number of rows affected.</returns>
    Task<int> DeleteAsync(LikeEntity like);

    /// <returns>Id exist item.</returns>
    Task<int> ExistAsync(LikeEntity like);
  }
}
