using FluentValidation;
using Uzumachi.McBuilds.Domain.Requests;
using Uzumachi.McBuilds.Domain.Types;

namespace Uzumachi.McBuilds.Api.Validators {

  public class PostUpdateRequestValidator : AbstractValidator<PostUpdateRequest> {

    public PostUpdateRequestValidator() {
      RuleFor(x => x.Text).NotEmpty().When(x => x.Attachments is null || x.Attachments.Count == 0);

      RuleForEach(x => x.Attachments)
        .ChildRules(attachments => {
          attachments.RuleFor(a => a.Value).NotEmpty();
          attachments.RuleFor(a => a.Type)
            .NotEmpty()
            .Must(type => AttachmentTypes.Parse(type) != 0)
            .WithMessage("'{PropertyName}' is unknown");
        });
    }
  }
}
