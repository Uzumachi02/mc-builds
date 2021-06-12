using Uzumachi.McBuilds.Domain.Types;

namespace Uzumachi.McBuilds.Core.Models {

  public class PostAttachmentCreateModel{

    private int? _attachmentTypeId;

    public int AttachmentTypeId => _attachmentTypeId ??= AttachmentTypes.Parse(Type);

    public string Type { get; set; }

    public string Value { get; set; }

    public int Priority { get; set; }

  }
}
