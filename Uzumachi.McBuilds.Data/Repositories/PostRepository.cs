using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Uzumachi.McBuilds.Data.Entities;
using Uzumachi.McBuilds.Data.Repositories.Interfaces;

namespace Uzumachi.McBuilds.Data.Repositories {

  public class PostRepository : IPostRepository {

    private readonly IDbConnection _dbConnection;

    public PostRepository(IDbConnection dbConnection) {
      _dbConnection = dbConnection;
    }


    public async Task<IEnumerable<PostEntity>> GetAll() {
      var sql = $"SELECT * FROM {PostEntity.TABLE} ORDER BY ID";

      return await _dbConnection.QueryAsync<PostEntity>(sql);
    }

    public async ValueTask<PostEntity> GetById(int id) {
      var sql = $"SELECT * FROM {PostEntity.TABLE} WHERE id = @id AND is_banned = false AND is_deleted = false LIMIT 1;";

      return await _dbConnection.QueryFirstOrDefaultAsync<PostEntity>(sql, new { id });
    }

    public async ValueTask<int> AddPostAsync(PostEntity newPost, CancellationToken token, IDbTransaction transaction = null) {

      var sql = $"INSERT INTO {PostEntity.TABLE} (user_id, text, close_comments) VALUES (@UserId, @Text, @CloseComments) RETURNING ID;";
      var resId = await _dbConnection.QueryFirstAsync<int>(
        new CommandDefinition(sql, newPost, transaction, cancellationToken: token)
      );

      newPost.Id = resId;

      return resId;
    }

    public async ValueTask<int> IncrementLikeForPost(int postID) {
      var sqlQuery = $"UPDATE {PostEntity.TABLE} SET like_count = like_count + 1 WHERE id = @postID RETURNING like_count;";

      return await _dbConnection.ExecuteScalarAsync<int>(sqlQuery, new { postID });
    }

    public async ValueTask<int> DecrementLikeForPost(int postID) {
      var sqlQuery = $"UPDATE {PostEntity.TABLE} SET like_count = like_count - 1 WHERE id = @postID RETURNING like_count;";

      return await _dbConnection.ExecuteScalarAsync<int>(sqlQuery, new { postID });
    }

    public Task<PostEntity> GetByIdForUser(int id, int userId) {
      var sql = $"SELECT * FROM {PostEntity.TABLE} WHERE id = @id AND user_id = @userId AND is_banned = false AND is_deleted = false LIMIT 1";

      return _dbConnection.QueryFirstOrDefaultAsync<PostEntity>(sql, new { id, userId });
    }
  }
}
