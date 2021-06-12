using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Domain.Entities;
using Uzumachi.McBuilds.Data.Repositories.Interfaces;

namespace Uzumachi.McBuilds.Data.Repositories {

  public class PostAttachmentRepository : IPostAttachmentRepository {

    private readonly IDbConnection _dbConnection;

    public PostAttachmentRepository(IDbConnection dbConnection) =>
      _dbConnection = dbConnection;

    public async Task<int> AddAsync(IEnumerable<PostAttachmentEntity> postAttachments, CancellationToken token, IDbTransaction transaction = null) {

      var sql = $"INSERT INTO {PostAttachmentEntity.TABLE} " +
        "(attachment_type_id, user_id, post_id, value, priority) VALUES " +
        "(@AttachmentTypeId, @UserId, @PostId, @Value, @Priority);";

      return await _dbConnection.ExecuteAsync(
        new CommandDefinition(sql, postAttachments, transaction, cancellationToken: token)
      );
    }

    public async Task<IEnumerable<PostAttachmentEntity>> GetListForPost(int postId, int limit = 10) {
      var sql = $"SELECT * FROM {PostAttachmentEntity.TABLE} WHERE post_id = @postId AND is_deleted = false ORDER BY priority, id LIMIT {limit};";

      return await _dbConnection.QueryAsync<PostAttachmentEntity>(sql, new { postId });
    }
  }
}
