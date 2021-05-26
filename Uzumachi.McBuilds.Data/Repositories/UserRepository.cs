using Uzumachi.McBuilds.Data.Interfaces;
using Uzumachi.McBuilds.Data.Repositories.Interfaces;

namespace Uzumachi.McBuilds.Data.Repositories {
  public class UserRepository : IUserRepository {

    private readonly ISqlConnection _connection;

    public UserRepository(ISqlConnection connection) {
      _connection = connection;
    }

  }
}
