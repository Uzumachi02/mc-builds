using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Domain.Requests;

namespace Uzumachi.McBuilds.Core.Mappers
{
    public static partial class CommentUpdateModelMapper
    {
        public static CommentUpdateModel AdaptToCommentUpdateModel(this CommentUpdateRequest p1)
        {
            return p1 == null ? null : new CommentUpdateModel() {Text = p1.Text};
        }
        public static CommentUpdateModel AdaptTo(this CommentUpdateRequest p2, CommentUpdateModel p3)
        {
            if (p2 == null)
            {
                return null;
            }
            CommentUpdateModel result = p3 ?? new CommentUpdateModel();
            
            result.Text = p2.Text;
            return result;
            
        }
    }
}