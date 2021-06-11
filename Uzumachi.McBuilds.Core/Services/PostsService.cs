using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Core.Services.Interfaces;
using Uzumachi.McBuilds.Data.Entities;
using Uzumachi.McBuilds.Data.Interfaces;

namespace Uzumachi.McBuilds.Core.Services {

  public class PostsService : IPostsService {

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PostsService(IUnitOfWork unitOfWork, IMapper mapper) =>
      (_unitOfWork, _mapper) = (unitOfWork, mapper);

    public async Task<PostModel> CreateAsync(CreatePostModel post, CancellationToken token) {

      var newPost = new PostEntity {
        Text = post.Text,
        CloseComments = post.CloseComments
      };

      if( post.PublishDate.HasValue ) {
        newPost.PublishDate = post.PublishDate.Value;
      }

      using var transaction = _unitOfWork.BeginTransaction();

      await _unitOfWork.Posts.AddPostAsync(newPost, token, transaction);

      var attachments = new List<PostAttachmentEntity>();

      if( post.Attachments is not null && post.Attachments.Count > 0 ) {
        foreach( var attachment in post.Attachments.OrderBy(x => x.Priority).Take(10) ) {
          var attachmentTypeId = AttachmentTypes.Parse(attachment.Type);

          if( attachmentTypeId == 0 ) {
            continue;
          }

          attachments.Add(new PostAttachmentEntity {
            AttachmentTypeId = attachmentTypeId,
            UserId = newPost.UserId,
            PostId = newPost.Id,
            Value = attachment.Value,
            Priority = attachment.Priority
          });
        }
      }


      if( attachments.Count > 0 ) {
        await _unitOfWork.PostAttachments.AddAsync(attachments, token, transaction);
      }

      transaction.Commit();

      var postAttachments = await _unitOfWork.PostAttachments.GetListForPost(newPost.Id);

      var resPost = _mapper.Map<PostModel>(newPost);
      resPost.Attachments = _mapper.Map<List<AttachmentModel>>(postAttachments);

      return resPost;
    }
  }
}
