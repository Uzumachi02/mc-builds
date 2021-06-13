using System.Collections.Generic;

namespace Uzumachi.McBuilds.Domain.Responses {

  public class ItemsResponse<T> {

    public int Count { get; set; }

    public IEnumerable<T> Items { get; set; }
  }
}
