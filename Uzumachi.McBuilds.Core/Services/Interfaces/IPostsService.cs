using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Domain.Dtos;
using Uzumachi.McBuilds.Domain.Requests;

namespace Uzumachi.McBuilds.Core.Services.Interfaces {

  public interface IPostsService {

    Task<PostDto> GetByIdAsync(int id, PostGetRequest req);

    Task<IEnumerable<PostDto>> GetListAsync(PostListRequest req);

    Task<PostDto> CreateAsync(PostCreateModel post, CancellationToken token);
  }
}
