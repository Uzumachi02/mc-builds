using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Uzumachi.McBuilds.Core.Models;

namespace Uzumachi.McBuilds.Api.Requests {

  public class CreatePostRequest {

    [MinLength(5)]
    public string Text { get; set; }

    public bool CloseComments { get; set; } = false;

    public DateTime? PublishDate { get; set; }

    public List<AttachmentModel> Attachments { get; set; }
  }
}
