using System;

namespace Uzumachi.McBuilds.Data.Entities {

  public class LikeTypeEntity {

    public const string TABLE = "public.like_types";

    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime CreateDate { get; set; }
  }

  public static class LikeTypes {
    public static readonly int Post = 1;
    public static readonly int Comment = 2;
  }
}
