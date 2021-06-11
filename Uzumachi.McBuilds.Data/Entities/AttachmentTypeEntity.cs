using System;

namespace Uzumachi.McBuilds.Data.Entities {

  public class AttachmentTypeEntity {

    public const string TABLE = "public.attachment_types";

    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime CreateDate { get; set; }
  }

  public static class AttachmentTypes {
    public static readonly int Link = 1;
    public static readonly int Image = 2;
    public static readonly int Video = 3;
  }
}
