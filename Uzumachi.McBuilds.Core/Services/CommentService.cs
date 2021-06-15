using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Exceptions;
using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Core.Services.Interfaces;
using Uzumachi.McBuilds.Data.Interfaces;
using Uzumachi.McBuilds.Domain.Entities;
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

      await _unitOfWork.Comments.DeleteAsync(dbComment, token, transaction);

      if( dbComment.ParentId > 0 ) {
        await _unitOfWork.Posts.DecrementCommentsAsync(dbComment.ItemId, token, transaction);
        await _unitOfWork.Comments.DecrementRepliesAsync(dbComment.ParentId, token, transaction);
      } else if( dbComment.ReplyCount == 0 ) {
        await _unitOfWork.Posts.DecrementCommentsAsync(dbComment.ItemId, token, transaction);
      }

      transaction.Commit();

      return dbComment.Id;
    }
  }
}
