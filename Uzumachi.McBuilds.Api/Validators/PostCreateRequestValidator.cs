using FluentValidation;
using Uzumachi.McBuilds.Domain.Requests;
using Uzumachi.McBuilds.Domain.Types;

namespace Uzumachi.McBuilds.Api.Validators {

  public class PostCreateRequestValidator : AbstractValidator<PostCreateRequest> {

    public PostCreateRequestValidator() {
      RuleFor(x => x.Text).NotEmpty().When(x => x.Attachments is null || x.Attachments.Count == 0);

      RuleFor(x => x.PublishDate)
        .GreaterThan(System.DateTime.UtcNow.AddMinutes(1))
        .When(x => x.PublishDate.HasValue);

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
