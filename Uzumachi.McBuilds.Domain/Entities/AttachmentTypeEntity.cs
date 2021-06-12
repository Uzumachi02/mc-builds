using System;

namespace Uzumachi.McBuilds.Domain.Entities {

  public class AttachmentTypeEntity {

    public const string TABLE = "public.attachment_types";

    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime CreateDate { get; set; }
  }
}
