using System;

namespace Uzumachi.McBuilds.Data.Entities {

  public class LikeEntity {

    public const string TABLE = "public.likes";

    public int Id { get; set; }

    public int LikeTypeId { get; set; }

    public int UserId { get; set; }

    public int ItemId { get; set; }

    public DateTime CreateDate { get; set; }
  }
}
