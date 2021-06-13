using System.Collections.Generic;

namespace Uzumachi.McBuilds.Core.Models {

  /// <summary>
  /// Model for update post
  /// </summary> 
  public class PostUpdateModel {

    public int Id { get; set; }

    /// <summary>
    /// user id
    /// </summary>
    public int UserId { get; set; }

    public string Text { get; set; }

    public bool CloseComments { get; set; }

    public List<PostAttachmentCreateModel> Attachments { get; set; }
  }
}
