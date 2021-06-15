namespace Uzumachi.McBuilds.Domain.Requests {

  public class CommentCreateRequest {

    public int ItemId { get; set; }

    public int ReplyId { get; set; }

    public int ReplyUserId { get; set; }

    public string Text { get; set; }
  }
}
