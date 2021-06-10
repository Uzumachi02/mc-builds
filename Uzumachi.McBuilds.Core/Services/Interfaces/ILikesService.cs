using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Models;

namespace Uzumachi.McBuilds.Core.Services.Interfaces {

  public interface ILikesService {

    Task<LikeResponseModel> LikePostAsync(LikeRequestModel req);

    Task<LikeResponseModel> DeleteLikePostAsync(LikeRequestModel req);
  }
}
