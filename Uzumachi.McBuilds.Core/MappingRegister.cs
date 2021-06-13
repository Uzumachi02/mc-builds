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

      config.AdaptFrom(nameof(PostEntity))
        .ForType<PostEntity>()
        .ForType<PostDto>();

      config.AdaptFrom(nameof(PostAttachmentEntity))
       .ForType<PostAttachmentEntity>()
       .ForType<PostAttachmentDto>();

      config.AdaptFrom(nameof(PostListRequest))
       .ForType<PostListRequest>()
       .ForType<PostFilters>();

      config.GenerateMapper("[name]Mapper")
        .ForType<PostCreateModel>()
        .ForType<PostDto>()
        .ForType<PostAttachmentDto>()
        .ForType<PostFilters>();
    }
  }
}
