using Uzumachi.McBuilds.Data.Interfaces;
using Uzumachi.McBuilds.Data.Repositories;
using Uzumachi.McBuilds.Data.Repositories.Interfaces;

namespace Uzumachi.McBuilds.Data {
  public class UnitOfWork : IUnitOfWork {

    private readonly ISqlConnection _connection;

    private IUserRepository _users;

    public IUserRepository Users => _users ??= new UserRepository(_connection);

    public UnitOfWork(ISqlConnection connection) {
      _connection = connection;
    }
  }
}
