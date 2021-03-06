using System;
using System.Collections.Generic;

namespace Uzumachi.McBuilds.Core.Models {

  /// <summary>
  /// Model for create post
  /// </summary> 
  public class PostCreateModel {

    /// <summary>
    /// user id
    /// </summary>
    public int UserId { get; set; }

    public string Text { get; set; }

    public bool CloseComments { get; set; }

    public DateTime? PublishDate { get; set; }

    public List<PostAttachmentCreateModel> Attachments { get; set; }
  }
}
