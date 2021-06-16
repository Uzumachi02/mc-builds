namespace Uzumachi.McBuilds.Domain.Requests {

  public class CommentListRequest {

    public int UserId { get; set; }

    public int ItemId { get; set; }

    public int ItemTypeId { get; set; }

    /// <summary>
    /// Идентификатор комментария, ветку которого нужно получить
    /// </summary>
    public int CommentId { get; set; }

    /// <summary>
    /// Идентификатор комментария, начиная с которого нужно вернуть список
    /// </summary>
    public int StartCommentId { get; set; }

    public int Limit { get; set; }

    public int Offset { get; set; }

    public string Sorting { get; set; }
  }
}
