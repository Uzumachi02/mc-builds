using Dapper;
using System.Data;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Data.Entities;
using Uzumachi.McBuilds.Data.Repositories.Interfaces;

namespace Uzumachi.McBuilds.Data.Repositories {

  public class LikeRepository : ILikeRepository {

    private readonly IDbConnection _dbConnection;

    public LikeRepository(IDbConnection dbConnection) {
      _dbConnection = dbConnection;
    }

    public async ValueTask<int> CreateAsync(LikeEntity newLike) {
      var sqlQuery = $"INSERT INTO {LikeEntity.TABLE} (like_type_id, user_id, item_id, create_date) VALUES (@LikeTypeId, @UserId, @ItemId, @CreateDate) RETURNING ID;";
      var resId = await _dbConnection.ExecuteScalarAsync<int>(sqlQuery, newLike);

      newLike.Id = resId;

      return resId;
    }

    public Task<int> DeleteAsync(LikeEntity like) {
      var sqlQuery = $"DELETE FROM {LikeEntity.TABLE} WHERE like_type_id = @LikeTypeId AND user_id = @UserId AND item_id = @ItemId;";

      return _dbConnection.ExecuteAsync(sqlQuery, like);
    }

    public Task<int> ExistAsync(LikeEntity like) {
      var sqlQuery = $"SELECT id FROM {LikeEntity.TABLE} WHERE like_type_id = @LikeTypeId AND user_id = @UserId AND item_id = @ItemId;";

      return _dbConnection.ExecuteScalarAsync<int>(sqlQuery, like);
    }
  }
}
