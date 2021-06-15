using Dapper;
using System;
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
      comment.CreateDate = comment.UpdateDate = DateTime.UtcNow;

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
      comment.UpdateDate = DateTime.UtcNow;

      var sql = $"UPDATE {CommentEntity.TABLE} SET text = @Text, update_date = @UpdateDate WHERE id = @Id;";

      return await _dbConnection.ExecuteAsync(
        new CommandDefinition(sql, comment, transaction, cancellationToken: token)
      );
    }

    public async Task<int> DeleteAsync(CommentEntity comment, CancellationToken token, IDbTransaction transaction = null) {
      comment.UpdateDate = DateTime.UtcNow;

      var sql = $"UPDATE {CommentEntity.TABLE} SET is_deleted = true, update_date = @UpdateDate WHERE id = @Id;";
      var res = await _dbConnection.ExecuteAsync(
        new CommandDefinition(sql, comment, transaction, cancellationToken: token)
      );

      comment.IsDeleted = true;

      return res;
    }

    public async Task<int> RestoreAsync(CommentEntity comment, CancellationToken token, IDbTransaction transaction = null) {
      comment.UpdateDate = DateTime.UtcNow;

      var sql = $"UPDATE {CommentEntity.TABLE} SET is_deleted = false, update_date = @UpdateDate WHERE id = @Id;";
      var res = await _dbConnection.ExecuteAsync(
        new CommandDefinition(sql, comment, transaction, cancellationToken: token)
      );

      comment.IsDeleted = false;

      return res;
    }

    public async Task<CommentEntity> GetByIdAsync(int id) {
      var sql = $"SELECT * FROM {CommentEntity.TABLE} WHERE id = @id AND is_banned = false AND is_deleted = false LIMIT 1;";

      return await _dbConnection.QueryFirstOrDefaultAsync<CommentEntity>(sql, new { id });
    }

    public async Task<CommentEntity> GetToRestoreAsync(int id, CancellationToken token = default) {
      var sql = $"SELECT id, parent_id, user_id, item_type_id, item_id, reply_count FROM {CommentEntity.TABLE} " +
        "WHERE id = @id AND is_banned = false AND is_deleted = true LIMIT 1;";

      return await _dbConnection.QueryFirstOrDefaultAsync<CommentEntity>(
        new CommandDefinition(sql, new { id }, cancellationToken: token));
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
      return await updateCounterAsync(commentId, "reply_count", true, token, transaction);
    }

    public async Task<int> DecrementRepliesAsync(int commentId, CancellationToken token, IDbTransaction transaction = null) {
      return await updateCounterAsync(commentId, "reply_count", false, token, transaction);
    }

    private Task<int> updateCounterAsync(int commentId, string counterName, bool increment = true, CancellationToken token = default, IDbTransaction transaction = null) {
      var sql= string.Format(
        "UPDATE {0} SET {1} = {1} " + (increment ? "+" : "-") + " 1 WHERE id = @commentId RETURNING {1};",
        CommentEntity.TABLE, counterName
      );

      return _dbConnection.ExecuteScalarAsync<int>(new CommandDefinition(sql, new { commentId }, transaction, cancellationToken: token));
    }
  }
}
