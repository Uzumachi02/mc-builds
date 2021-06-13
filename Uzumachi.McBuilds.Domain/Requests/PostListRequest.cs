namespace Uzumachi.McBuilds.Domain.Requests {

  public class PostListRequest {

    public int Limit { get; set; }

    public int Offset { get; set; }

    public int UserId { get; set; }

    public string Sorting { get; set; }
  }
}
