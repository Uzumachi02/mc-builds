using System;
using Uzumachi.McBuilds.Data.Entities;

namespace Uzumachi.McBuilds.Core.Models {

  public class PostModel {

    public int Id { get; set; }

    public int UserId { get; set; }

    public string Description { get; set; }

    public int ViewCount { get; set; }

    public int LikeCount { get; set; }

    public int CommentCount { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime PublishDate { get; set; }

    public DateTime UpdateDate { get; set; }


    public PostModel(PostEntity postEntity) {
      Id = postEntity.Id;
      UserId = postEntity.UserId;
      Description = postEntity.Description;
    }
  }
}
