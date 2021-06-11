using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Core.Services.Interfaces;
using Uzumachi.McBuilds.Data.Entities;
using Uzumachi.McBuilds.Data.Interfaces;

namespace Uzumachi.McBuilds.Core.Services {

  public class PostsService : IPostsService {

    private readonly IUnitOfWork _unitOfWork;

    public PostsService(IUnitOfWork unitOfWork) {
      _unitOfWork = unitOfWork;
    }

    public async Task<PostModel> CreateAsync(CreatePostModel post) {

      var newPost = new PostEntity {
        Text = post.Text,
        CloseComments = post.CloseComments
      };

      if( post.PublishDate.HasValue ) {
        newPost.PublishDate = post.PublishDate.Value;
      }

      await _unitOfWork.Posts.AddPostAsync(newPost);

      var resPost = new PostModel(newPost);

      return resPost;
    }
  }
}
