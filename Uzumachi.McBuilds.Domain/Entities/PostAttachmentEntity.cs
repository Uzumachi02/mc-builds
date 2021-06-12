using System;

namespace Uzumachi.McBuilds.Domain.Entities {

  public class PostAttachmentEntity {

    public const string TABLE = "public.post_attachments";

    public int Id { get; set; }

    public int AttachmentTypeId { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }

    public string Value { get; set; }

    public int Priority { get; set; }

    public string Params { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime DeleteDate { get; set; }
  }
}
