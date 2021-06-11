using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Models;

namespace Uzumachi.McBuilds.Core.Services.Interfaces {

  public interface IPostsService {

    Task<PostModel> CreateAsync(CreatePostModel post, CancellationToken token);
  }
}
