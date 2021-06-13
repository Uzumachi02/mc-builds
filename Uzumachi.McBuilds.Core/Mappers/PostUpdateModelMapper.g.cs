using System.Collections.Generic;
using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Domain.Requests;

namespace Uzumachi.McBuilds.Core.Mappers
{
    public static partial class PostUpdateModelMapper
    {
        public static PostUpdateModel AdaptToPostUpdateModel(this PostUpdateRequest p1)
        {
            return p1 == null ? null : new PostUpdateModel()
            {
                Text = p1.Text,
                CloseComments = p1.CloseComments,
                Attachments = funcMain1(p1.Attachments)
            };
        }
        public static PostUpdateModel AdaptTo(this PostUpdateRequest p3, PostUpdateModel p4)
        {
            if (p3 == null)
            {
                return null;
            }
            PostUpdateModel result = p4 ?? new PostUpdateModel();
            
            result.Text = p3.Text;
            result.CloseComments = p3.CloseComments;
            result.Attachments = funcMain2(p3.Attachments, result.Attachments);
            return result;
            
        }
        
        private static List<PostAttachmentCreateModel> funcMain1(List<PostAttachmentCreateRequest> p2)
        {
            if (p2 == null)
            {
                return null;
            }
            List<PostAttachmentCreateModel> result = new List<PostAttachmentCreateModel>(p2.Count);
            
            int i = 0;
            int len = p2.Count;
            
            while (i < len)
            {
                PostAttachmentCreateRequest item = p2[i];
                result.Add(item == null ? null : new PostAttachmentCreateModel()
                {
                    Type = item.Type,
                    Value = item.Value,
                    Priority = item.Priority
                });
                i++;
            }
            return result;
            
        }
        
        private static List<PostAttachmentCreateModel> funcMain2(List<PostAttachmentCreateRequest> p5, List<PostAttachmentCreateModel> p6)
        {
            if (p5 == null)
            {
                return null;
            }
            List<PostAttachmentCreateModel> result = new List<PostAttachmentCreateModel>(p5.Count);
            
            int i = 0;
            int len = p5.Count;
            
            while (i < len)
            {
                PostAttachmentCreateRequest item = p5[i];
                result.Add(item == null ? null : new PostAttachmentCreateModel()
                {
                    Type = item.Type,
                    Value = item.Value,
                    Priority = item.Priority
                });
                i++;
            }
            return result;
            
        }
    }
}