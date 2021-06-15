using Dapper;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Data.Repositories.Interfaces;
using Uzumachi.McBuilds.Domain.Entities;

namespace Uzumachi.McBuilds.Data.Repositories {

  public class CommentRepository : ICommentRepository {

    private readonly IDbConnection _dbConnection;

    public CommentRepository(IDbConnection dbConnection) =>
      _dbConnection = dbConnection;

    public async Task<int> CreateAsync(CommentEntity comment, CancellationToken token, IDbTransaction transaction = null) {
      var sql = $"INSERT INTO {CommentEntity.TABLE} " +
        "(parent_id, user_id, item_type_id, item_id, reply_id, reply_user_id, text, create_date, update_date) VALUES " +
        "(@ParentId, @UserId, @ItemTypeId, @ItemId, @ReplyId, @ReplyUserId, @Text, @CreateDate, @UpdateDate) RETURNING ID;";

      var resId = await _dbConnection.ExecuteScalarAsync<int>(
        new CommandDefinition(sql, comment, transaction, cancellationToken: token)
      );

      comment.Id = resId;

      return resId;
    }

    public async Task<int> UpdateAsync(CommentEntity comment, CancellationToken token, IDbTransaction transaction = null) {
      var sql = $"UPDATE {CommentEntity.TABLE} SET text = @Text, update_date = @UpdateDate WHERE id = @Id;";

      return await _dbConnection.ExecuteAsync(
        new CommandDefinition(sql, comment, transaction, cancellationToken: token)
      );
    }

    public async Task<CommentEntity> GetByIdAsync(int id) {
      var sql = $"SELECT * FROM {CommentEntity.TABLE} WHERE id = @id AND is_banned = false AND is_deleted = false LIMIT 1;";

      return await _dbConnection.QueryFirstOrDefaultAsync<CommentEntity>(sql, new { id });
    }

    public async Task<int> GetTopParentIdByReplyIdAsync(int replyId) {
      var sql = string.Format("WITH RECURSIVE cs AS (" +
        "SELECT * FROM {0} WHERE id = @replyId " +
        "UNION ALL " +
        "SELECT c.* FROM {0} c, cs WHERE cs.parent_id = c.id" +
        ") SELECT cs.id FROM cs WHERE cs.parent_id = 0 ORDER BY cs.id DESC LIMIT 1;", CommentEntity.TABLE);

      return await _dbConnection.ExecuteScalarAsync<int>(sql, new { replyId });
    }

    public async Task<int> IncrementRepliesAsync(int commentId, CancellationToken token, IDbTransaction transaction = null) {
      return await incrementCounterAsync(commentId, "reply_count", token, transaction);
    }

    private Task<int> incrementCounterAsync(int commentId, string counterName, CancellationToken token, IDbTransaction transaction = null) {
      var sqlQuery = string.Format(
        "UPDATE {0} SET {1} = {1} + 1 WHERE id = @commentId RETURNING {1};",
        CommentEntity.TABLE, counterName
      );

      return _dbConnection.ExecuteScalarAsync<int>(new CommandDefinition(sqlQuery, new { commentId }, transaction, cancellationToken: token));
    }
  }
}
