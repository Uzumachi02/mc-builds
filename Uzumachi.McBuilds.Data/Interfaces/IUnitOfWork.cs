using Uzumachi.McBuilds.Data.Repositories.Interfaces;

namespace Uzumachi.McBuilds.Data.Interfaces {

  public interface IUnitOfWork {

    IUserRepository Users { get; }

    IPostRepository Posts { get; }
  }
}
