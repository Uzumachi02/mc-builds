using Mapster;
using Uzumachi.McBuilds.Core.Models;
using Uzumachi.McBuilds.Data.Filters;
using Uzumachi.McBuilds.Domain.Dtos;
using Uzumachi.McBuilds.Domain.Entities;
using Uzumachi.McBuilds.Domain.Requests;

namespace Uzumachi.McBuilds.Core {

  public class MappingRegister : ICodeGenerationRegister {

    public void Register(CodeGenerationConfig config) {

      config.AdaptFrom(nameof(PostCreateRequest))
        .ForType<PostCreateRequest>()
        .ForType<PostCreateModel>();

      config.AdaptFrom(nameof(PostUpdateRequest))
        .ForType<PostUpdateRequest>()
        .ForType<PostUpdateModel>();

      config.AdaptFrom(nameof(PostEntity))
        .ForType<PostEntity>()
        .ForType<PostDto>();

      config.AdaptFrom(nameof(PostAttachmentEntity))
       .ForType<PostAttachmentEntity>()
       .ForType<PostAttachmentDto>();

      config.AdaptFrom(nameof(PostListRequest))
       .ForType<PostListRequest>()
       .ForType<PostFilters>();

      config.AdaptFrom(nameof(CommentCreateRequest))
       .ForType<CommentCreateRequest>()
       .ForType<CommentCreateModel>();

      config.AdaptFrom(nameof(CommentUpdateRequest))
       .ForType<CommentUpdateRequest>()
       .ForType<CommentUpdateModel>();

      config.AdaptFrom(nameof(CommentEntity))
       .ForType<CommentEntity>()
       .ForType<CommentDto>();

      config.GenerateMapper("[name]Mapper")
        .ForType<PostCreateModel>()
        .ForType<PostUpdateModel>()
        .ForType<PostDto>()
        .ForType<PostAttachmentDto>()
        .ForType<PostFilters>()
        .ForType<CommentCreateModel>()
        .ForType<CommentUpdateModel>()
        .ForType<CommentDto>();
    }
  }
}
