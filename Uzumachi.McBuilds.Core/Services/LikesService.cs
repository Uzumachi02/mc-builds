using System;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Core.Services.Interfaces;
using Uzumachi.McBuilds.Data.Entities;
using Uzumachi.McBuilds.Data.Interfaces;

namespace Uzumachi.McBuilds.Core.Services {

  public class LikesService : ILikesService {

    private readonly IUnitOfWork _unitOfWork;

    public LikesService(IUnitOfWork unitOfWork) =>
      (_unitOfWork) = (unitOfWork);

    public async Task<LikeResponseModel> LikePostAsync(LikeRequestModel req) {
      var post = await _unitOfWork.Posts.GetByIdForUser(req.ItemId, req.UserId);

      if( post is null ) {
        throw new Exception("Post not found");
      }

      var response = new LikeResponseModel {
        Likes = post.LikeCount
      };

      var newLike = new LikeEntity {
        LikeTypeId = LikeTypes.Post,
        UserId = req.UserId,
        ItemId = req.ItemId
      };

      int existId = await _unitOfWork.Likes.ExistAsync(newLike);

      if( existId > 0 ) {
        return response;
      }

      newLike.CreateDate = DateTime.UtcNow;
      await _unitOfWork.Likes.CreateAsync(newLike);

      if( newLike.Id > 0 ) {
        response.Likes = await _unitOfWork.Posts.IncrementLikeForPost(newLike.ItemId);
      }

      return response;
    }

    public async Task<LikeResponseModel> DeleteLikePostAsync(LikeRequestModel req) {
      var post = await _unitOfWork.Posts.GetByIdForUser(req.ItemId, req.UserId);

      if( post is null ) {
        throw new Exception("Post not found");
      }

      var response = new LikeResponseModel {
        Likes = post.LikeCount
      };

      var like = new LikeEntity {
        LikeTypeId = LikeTypes.Post,
        UserId = req.UserId,
        ItemId = req.ItemId
      };

      int affected = await _unitOfWork.Likes.DeleteAsync(like);

      if( affected > 0 ) {
        response.Likes = await _unitOfWork.Posts.DecrementLikeForPost(like.ItemId);
      }

      return response;
    }
  }
}
