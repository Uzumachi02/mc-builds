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

    public static int Parse(string type) =>
      (type.ToLower().Trim()) switch {
        "link" => 1,
        "image" => 2,
        "video" => 3,
        _ => 0,
      };

    public static string ToString(int typeId) =>
      (typeId) switch {
        1 => "link",
        2 => "image",
        3 => "video",
        _ => "unknown",
      };
  }
}
