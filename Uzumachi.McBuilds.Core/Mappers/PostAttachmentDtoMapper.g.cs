using Uzumachi.McBuilds.Domain.Dtos;
using Uzumachi.McBuilds.Domain.Entities;

namespace Uzumachi.McBuilds.Core.Mappers
{
    public static partial class PostAttachmentDtoMapper
    {
        public static PostAttachmentDto AdaptToPostAttachmentDto(this PostAttachmentEntity p1)
        {
            return p1 == null ? null : new PostAttachmentDto()
            {
                Type = p1.Type,
                Value = p1.Value,
                Priority = p1.Priority
            };
        }
        public static PostAttachmentDto AdaptTo(this PostAttachmentEntity p2, PostAttachmentDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            PostAttachmentDto result = p3 ?? new PostAttachmentDto();
            
            result.Type = p2.Type;
            result.Value = p2.Value;
            result.Priority = p2.Priority;
            return result;
            
        }
    }
}