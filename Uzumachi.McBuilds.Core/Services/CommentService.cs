using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Exceptions;
using Uzumachi.McBuilds.Core.Mappers;
using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Core.Services.Interfaces;
using Uzumachi.McBuilds.Data.Interfaces;
using Uzumachi.McBuilds.Domain.Dtos;
using Uzumachi.McBuilds.Domain.Entities;
using Uzumachi.McBuilds.Domain.Requests;
using Uzumachi.McBuilds.Domain.Responses;
using Uzumachi.McBuilds.Domain.Types;

namespace Uzumachi.McBuilds.Core.Services {

  public class CommentService : ICommentService {

    private readonly IUnitOfWork _unitOfWork;

    public CommentService(IUnitOfWork unitOfWork) =>
      _unitOfWork = unitOfWork;

    public async Task<int> CreateForPostAsync(CommentCreateModel comment, CancellationToken token) {
      var dbPost = await _unitOfWork.Posts.GetById(comment.ItemId)
        ?? throw new NotFoundCoreException("Post", comment.ItemId);

      comment.ItemTypeId = ItemTypes.Post;

      var newComment = new CommentEntity {
        UserId = comment.UserId,
        ItemTypeId = comment.ItemTypeId,
        ItemId = comment.ItemId,
        ReplyId = comment.ReplyId,
        ReplyUserId = comment.ReplyUserId,
        Text = comment.Text
      };

      if( newComment.ReplyId > 0 ) {
        newComment.ParentId = await _unitOfWork.Comments.GetTopParentIdByReplyIdAsync(newComment.ReplyId);

        if( newComment.ParentId == 0 ) {
          throw new NotFoundCoreException("Comment", newComment.ParentId);
        }
      }

      using var transaction = _unitOfWork.BeginTransaction();

      int commentId = await _unitOfWork.Comments.CreateAsync(newComment, token, transaction);

      await _unitOfWork.Posts.IncrementCommentsAsync(newComment.ItemId, token, transaction);

      if( newComment.ParentId > 0 ) {
        await _unitOfWork.Comments.IncrementRepliesAsync(newComment.ParentId, token, transaction);
      }

      transaction.Commit();

      return commentId;
    }

    public async Task<int> UpdateAsync(CommentUpdateModel comment, CancellationToken token) {
      var dbComment = await _unitOfWork.Comments.GetByIdAsync(comment.Id)
        ?? throw new NotFoundCoreException("Comment", comment.Id);

      if( dbComment.UserId != comment.UserId ) {
        throw new ForbiddenAccessCoreException();
      }

      dbComment.Text = comment.Text;

      await _unitOfWork.Comments.UpdateAsync(dbComment, token);

      return dbComment.Id;
    }

    public async Task<int> DeleteAsync(DeleteModel req, CancellationToken token) {
      var dbComment = await _unitOfWork.Comments.GetByIdAsync(req.ItemId)
        ?? throw new NotFoundCoreException("Comment", req.ItemId);

      if( dbComment.UserId != req.UserId ) {
        throw new ForbiddenAccessCoreException();
      }

      using var transaction = _unitOfWork.BeginTransaction();

      var affected = await _unitOfWork.Comments.DeleteAsync(dbComment, token, transaction);

      if( affected > 0 ) {
        await updateCountersAsync(dbComment, false, token, transaction);
      }

      transaction.Commit();

      return dbComment.Id;
    }

    public async Task<int> RestoreAsync(RestoreModel req, CancellationToken token) {
      var dbComment = await _unitOfWork.Comments.GetToRestoreAsync(req.ItemId, token)
        ?? throw new NotFoundCoreException("Comment", req.ItemId);

      if( dbComment.UserId != req.UserId ) {
        throw new ForbiddenAccessCoreException();
      }

      using var transaction = _unitOfWork.BeginTransaction();

      var affected = await _unitOfWork.Comments.RestoreAsync(dbComment, token, transaction);

      if( affected > 0 ) {
        await updateCountersAsync(dbComment, true, token, transaction);
      }

      transaction.Commit();

      return dbComment.Id;
    }

    public async Task<CommentDto> GetByIdAsync(int id, PostGetRequest req) {
      var dbComment = await _unitOfWork.Comments.GetByIdAsync(id)
        ?? throw new NotFoundCoreException("Comment", id);

      var comment = dbComment.AdaptToCommentDto();

      return comment;
    }

    public async Task<ItemsResponse<CommentDto>> GetListAsync(CommentListRequest req) {
      var filters = req.AdaptToCommentFilters();
      var countComments = await _unitOfWork.Comments.GetListCountAsync(filters);

      var result = new ItemsResponse<CommentDto> {
        Count = countComments
      };

      if( countComments == 0 ) {
        result.Items = System.Array.Empty<CommentDto>();
        return result;
      }

      var dbComments = await _unitOfWork.Comments.GetListAsync(filters);
      var comments = dbComments.Select(x => x.AdaptToCommentDto()).ToArray();

      result.Items = comments;
      return result;
    }

    private async Task<bool> updateCountersAsync(CommentEntity comment, bool increment = true, CancellationToken token = default, IDbTransaction transaction = null) {
      var updateCounterItem = false;

      if( comment.ParentId > 0 ) {
        updateCounterItem = true;

        if( increment ) {
          await _unitOfWork.Comments.IncrementRepliesAsync(comment.ParentId, token, transaction);
        } else {
          await _unitOfWork.Comments.DecrementRepliesAsync(comment.ParentId, token, transaction);
        }

      } else if( comment.ReplyCount == 0 ) {
        updateCounterItem = true;
      }

      if( updateCounterItem ) {
        Task<int> itemTask = null;

        if( comment.ItemTypeId == ItemTypes.Post ) {
          itemTask = increment
            ? _unitOfWork.Posts.IncrementCommentsAsync(comment.ItemId, token, transaction)
            : _unitOfWork.Posts.DecrementCommentsAsync(comment.ItemId, token, transaction);
        }

        if( itemTask != null ) {
          await itemTask;
        }
      }

      return true;
    }
  }
}
