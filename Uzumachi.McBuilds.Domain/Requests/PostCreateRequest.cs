using System;
using System.Collections.Generic;

namespace Uzumachi.McBuilds.Domain.Requests {

  public class PostCreateRequest {

    public string Text { get; set; }

    public bool CloseComments { get; set; } = false;

    public DateTime? PublishDate { get; set; }

    public List<PostAttachmentCreateRequest> Attachments { get; set; }
  }
}
