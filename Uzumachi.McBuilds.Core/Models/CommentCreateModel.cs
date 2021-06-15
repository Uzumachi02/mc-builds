namespace Uzumachi.McBuilds.Core.Models {

  public class CommentCreateModel {

    public int UserId { get; set; }

    public int ItemId { get; set; }

    public int ItemTypeId { get; set; }

    public int ReplyId { get; set; }

    public int ReplyUserId { get; set; }

    public string Text { get; set; }
  }
}
