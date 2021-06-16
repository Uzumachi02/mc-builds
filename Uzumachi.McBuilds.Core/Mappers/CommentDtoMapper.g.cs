using Uzumachi.McBuilds.Domain.Dtos;
using Uzumachi.McBuilds.Domain.Entities;

namespace Uzumachi.McBuilds.Core.Mappers
{
    public static partial class CommentDtoMapper
    {
        public static CommentDto AdaptToCommentDto(this CommentEntity p1)
        {
            return p1 == null ? null : new CommentDto()
            {
                Id = p1.Id,
                ParentId = p1.ParentId,
                UserId = p1.UserId,
                ItemTypeId = p1.ItemTypeId,
                ItemId = p1.ItemId,
                ReplyId = p1.ReplyId,
                ReplyUserId = p1.ReplyUserId,
                Text = p1.Text,
                LikeCount = p1.LikeCount,
                ReplyCount = p1.ReplyCount,
                CreateDate = p1.CreateDate
            };
        }
        public static CommentDto AdaptTo(this CommentEntity p2, CommentDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            CommentDto result = p3 ?? new CommentDto();
            
            result.Id = p2.Id;
            result.ParentId = p2.ParentId;
            result.UserId = p2.UserId;
            result.ItemTypeId = p2.ItemTypeId;
            result.ItemId = p2.ItemId;
            result.ReplyId = p2.ReplyId;
            result.ReplyUserId = p2.ReplyUserId;
            result.Text = p2.Text;
            result.LikeCount = p2.LikeCount;
            result.ReplyCount = p2.ReplyCount;
            result.CreateDate = p2.CreateDate;
            return result;
            
        }
    }
}