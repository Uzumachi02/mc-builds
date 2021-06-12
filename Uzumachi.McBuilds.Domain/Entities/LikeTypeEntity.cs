using System;

namespace Uzumachi.McBuilds.Domain.Entities {

  public class LikeTypeEntity {

    public const string TABLE = "public.like_types";

    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime CreateDate { get; set; }
  }
}
