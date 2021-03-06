using System;

namespace Uzumachi.McBuilds.Domain.Entities {

  public class PostEntity {

    public const string TABLE = "public.posts";

    public int Id { get; set; }

    public int UserId { get; set; }

    public string Text { get; set; }

    public bool CloseComments { get; set; }

    public int ViewCount { get; set; }

    public int LikeCount { get; set; }

    public int CommentCount { get; set; }

    public bool IsBanned { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime PublishDate { get; set; }

    public DateTime UpdateDate { get; set; }
  }
}
