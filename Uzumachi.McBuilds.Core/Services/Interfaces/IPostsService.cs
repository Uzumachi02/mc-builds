using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Domain.Dtos;

namespace Uzumachi.McBuilds.Core.Services.Interfaces {

  public interface IPostsService {

    Task<PostDto> CreateAsync(PostCreateModel post, CancellationToken token);
  }
}
