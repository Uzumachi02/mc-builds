using Uzumachi.McBuilds.Data.Filters;
using Uzumachi.McBuilds.Domain.Requests;

namespace Uzumachi.McBuilds.Core.Mappers
{
    public static partial class CommentFiltersMapper
    {
        public static CommentFilters AdaptToCommentFilters(this CommentListRequest p1)
        {
            return p1 == null ? null : new CommentFilters()
            {
                UserId = p1.UserId,
                ItemId = p1.ItemId,
                ItemTypeId = p1.ItemTypeId,
                CommentId = p1.CommentId,
                StartCommentId = p1.StartCommentId,
                Limit = p1.Limit,
                Offset = p1.Offset,
                Sorting = p1.Sorting
            };
        }
        public static CommentFilters AdaptTo(this CommentListRequest p2, CommentFilters p3)
        {
            if (p2 == null)
            {
                return null;
            }
            CommentFilters result = p3 ?? new CommentFilters();
            
            result.UserId = p2.UserId;
            result.ItemId = p2.ItemId;
            result.ItemTypeId = p2.ItemTypeId;
            result.CommentId = p2.CommentId;
            result.StartCommentId = p2.StartCommentId;
            result.Limit = p2.Limit;
            result.Offset = p2.Offset;
            result.Sorting = p2.Sorting;
            return result;
            
        }
    }
}