using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Domain.Requests;

namespace Uzumachi.McBuilds.Core.Mappers
{
    public static partial class CommentCreateModelMapper
    {
        public static CommentCreateModel AdaptToCommentCreateModel(this CommentCreateRequest p1)
        {
            return p1 == null ? null : new CommentCreateModel()
            {
                ItemId = p1.ItemId,
                ReplyId = p1.ReplyId,
                ReplyUserId = p1.ReplyUserId,
                Text = p1.Text
            };
        }
        public static CommentCreateModel AdaptTo(this CommentCreateRequest p2, CommentCreateModel p3)
        {
            if (p2 == null)
            {
                return null;
            }
            CommentCreateModel result = p3 ?? new CommentCreateModel();
            
            result.ItemId = p2.ItemId;
            result.ReplyId = p2.ReplyId;
            result.ReplyUserId = p2.ReplyUserId;
            result.Text = p2.Text;
            return result;
            
        }
    }
}