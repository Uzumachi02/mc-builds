using System.Threading.Tasks;
using Uzumachi.McBuilds.Domain.Entities;

namespace Uzumachi.McBuilds.Data.Repositories.Interfaces {

  public interface ICommentRepository {

    Task<CommentEntity> GetByIdAsync(int id);
  }
}
