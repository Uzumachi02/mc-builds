using System.Collections.Generic;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Data.Entities;

namespace Uzumachi.McBuilds.Data.Repositories.Interfaces {

  public interface IUserRepository {

    Task<IEnumerable<UserEntity>> GetAll();

    ValueTask<UserEntity> GetById(int id);
  }
}
