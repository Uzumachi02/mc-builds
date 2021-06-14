using Dapper;
using System.Data;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Data.Repositories.Interfaces;
using Uzumachi.McBuilds.Domain.Entities;

namespace Uzumachi.McBuilds.Data.Repositories {

  public class CommentRepository : ICommentRepository {

    private readonly IDbConnection _dbConnection;

    public CommentRepository(IDbConnection dbConnection) =>
      _dbConnection = dbConnection;

    public async Task<CommentEntity> GetByIdAsync(int id) {
      var sql = $"SELECT * FROM {CommentEntity.TABLE} WHERE id = @id AND is_banned = false AND is_deleted = false LIMIT 1;";

      return await _dbConnection.QueryFirstOrDefaultAsync<CommentEntity>(sql, new { id });
    }
  }
}
