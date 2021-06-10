using System.Data;
using Uzumachi.McBuilds.Data.Interfaces;
using Uzumachi.McBuilds.Data.Repositories;
using Uzumachi.McBuilds.Data.Repositories.Interfaces;

namespace Uzumachi.McBuilds.Data {

  public class UnitOfWork : IUnitOfWork {

    private readonly IDbConnection _dbConnection;


    private IUserRepository _users;

    private IPostRepository _posts;

    public ILikeRepository _likes;


    public IUserRepository Users => _users ??= new UserRepository(_dbConnection);

    public IPostRepository Posts => _posts ??= new PostRepository(_dbConnection);

    public ILikeRepository Likes => _likes ??= new LikeRepository(_dbConnection);

    public UnitOfWork(IDbConnection dbConnection) {
      _dbConnection = dbConnection;
    }
  }
}
