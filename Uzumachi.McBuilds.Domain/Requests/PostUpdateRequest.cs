using System.Collections.Generic;

namespace Uzumachi.McBuilds.Domain.Requests {

  public class PostUpdateRequest {

    public string Text { get; set; }

    public bool CloseComments { get; set; } = false;

    public List<PostAttachmentCreateRequest> Attachments { get; set; }
  }
}
