using System;
using System.Collections.Generic;

namespace Uzumachi.McBuilds.Domain.Dtos {

  public class PostDto {

    public int Id { get; set; }

    public int UserId { get; set; }

    public string Text { get; set; }

    public bool CloseComments { get; set; }

    public int ViewCount { get; set; }

    public int LikeCount { get; set; }

    public int CommentCount { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime PublishDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public IEnumerable<PostAttachmentDto> Attachments { get; set; }
  }
}
