using Uzumachi.McBuilds.Domain.Dtos;
using Uzumachi.McBuilds.Domain.Entities;

namespace Uzumachi.McBuilds.Core.Mappers
{
    public static partial class PostDtoMapper
    {
        public static PostDto AdaptToPostDto(this PostEntity p1)
        {
            return p1 == null ? null : new PostDto()
            {
                Id = p1.Id,
                UserId = p1.UserId,
                Text = p1.Text,
                CloseComments = p1.CloseComments,
                ViewCount = p1.ViewCount,
                LikeCount = p1.LikeCount,
                CommentCount = p1.CommentCount,
                CreateDate = p1.CreateDate,
                PublishDate = p1.PublishDate,
                UpdateDate = p1.UpdateDate
            };
        }
        public static PostDto AdaptTo(this PostEntity p2, PostDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            PostDto result = p3 ?? new PostDto();
            
            result.Id = p2.Id;
            result.UserId = p2.UserId;
            result.Text = p2.Text;
            result.CloseComments = p2.CloseComments;
            result.ViewCount = p2.ViewCount;
            result.LikeCount = p2.LikeCount;
            result.CommentCount = p2.CommentCount;
            result.CreateDate = p2.CreateDate;
            result.PublishDate = p2.PublishDate;
            result.UpdateDate = p2.UpdateDate;
            return result;
            
        }
    }
}