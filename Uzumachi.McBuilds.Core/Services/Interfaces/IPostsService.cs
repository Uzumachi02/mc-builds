using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Domain.Dtos;
using Uzumachi.McBuilds.Domain.Requests;
using Uzumachi.McBuilds.Domain.Responses;

namespace Uzumachi.McBuilds.Core.Services.Interfaces {

  public interface IPostsService {

    Task<PostDto> GetByIdAsync(int id, PostGetRequest req);

    Task<ItemsResponse<PostDto>> GetListAsync(PostListRequest req);

    Task<PostDto> CreateAsync(PostCreateModel post, CancellationToken token);

    Task<int> DeleteAsync(DeleteModel req);

    Task<int> RestoreAsync(RestoreModel req);
  }
}
