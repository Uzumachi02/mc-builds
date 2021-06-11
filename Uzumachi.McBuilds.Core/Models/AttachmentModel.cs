using AutoMapper;
using Uzumachi.McBuilds.Core.Mappings;
using Uzumachi.McBuilds.Data.Entities;

namespace Uzumachi.McBuilds.Core.Models {

  public class AttachmentModel : IMapFrom<PostAttachmentEntity> {

    public string Type { get; set; }

    public string Value { get; set; }

    public int Priority { get; set; }

    public void Mapping(Profile profile) {
      profile.CreateMap<PostAttachmentEntity, AttachmentModel>()
        .ForMember(x => x.Type, conf => {
          conf.MapFrom(x => AttachmentTypes.ToString(x.AttachmentTypeId));
        });
    }
  }
}
