using Uzumachi.McBuilds.Data.Filters;
using Uzumachi.McBuilds.Domain.Requests;

namespace Uzumachi.McBuilds.Core.Mappers
{
    public static partial class PostFiltersMapper
    {
        public static PostFilters AdaptToPostFilters(this PostListRequest p1)
        {
            return p1 == null ? null : new PostFilters()
            {
                UserId = p1.UserId,
                Limit = p1.Limit,
                Offset = p1.Offset,
                Sorting = p1.Sorting
            };
        }
        public static PostFilters AdaptTo(this PostListRequest p2, PostFilters p3)
        {
            if (p2 == null)
            {
                return null;
            }
            PostFilters result = p3 ?? new PostFilters();
            
            result.UserId = p2.UserId;
            result.Limit = p2.Limit;
            result.Offset = p2.Offset;
            result.Sorting = p2.Sorting;
            return result;
            
        }
    }
}