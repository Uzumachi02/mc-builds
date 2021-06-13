﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Exceptions;
using Uzumachi.McBuilds.Core.Mappers;
using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Core.Services.Interfaces;
using Uzumachi.McBuilds.Data.Interfaces;
using Uzumachi.McBuilds.Domain.Dtos;
using Uzumachi.McBuilds.Domain.Entities;
using Uzumachi.McBuilds.Domain.Requests;

namespace Uzumachi.McBuilds.Core.Services {

  public class PostsService : IPostsService {

    private readonly IUnitOfWork _unitOfWork;

    public PostsService(IUnitOfWork unitOfWork) =>
      _unitOfWork = unitOfWork;


    public async Task<PostDto> GetByIdAsync(int id, PostGetRequest req) {
      var dbPost = await _unitOfWork.Posts.GetById(id)
        ?? throw new NotFoundCoreException("Post", id);

      var post = dbPost.AdaptToPostDto();

      if( req.Extendet > 0 ) {
        var postAttachments = await _unitOfWork.PostAttachments.GetListForPost(id);

        post.Attachments = postAttachments.Select(a => a.AdaptToPostAttachmentDto()).ToArray();
      }

      return post;
    }

    public async Task<IEnumerable<PostDto>> GetListAsync(PostListRequest req) {
      var dbPosts = await _unitOfWork.Posts.GetAll();
      var posts = dbPosts.Select(x => x.AdaptToPostDto()).ToArray();

      foreach( var post in posts ) {
        var postAttachments = await _unitOfWork.PostAttachments.GetListForPost(post.Id);
        post.Attachments = postAttachments.Select(a => a.AdaptToPostAttachmentDto()).ToArray();
      }

      return posts;
    }

    public async Task<PostDto> CreateAsync(PostCreateModel post, CancellationToken token) {

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
          if( attachment.AttachmentTypeId == 0 ) {
            continue;
          }

          attachments.Add(new PostAttachmentEntity {
            AttachmentTypeId = attachment.AttachmentTypeId,
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
      var resPost = newPost.AdaptToPostDto();

      resPost.Attachments = postAttachments.Select(a => a.AdaptToPostAttachmentDto()).ToArray();

      return resPost;
    }
  }
}
