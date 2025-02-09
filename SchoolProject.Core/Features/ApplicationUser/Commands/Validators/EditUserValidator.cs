using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators
{
    public class EditUserValidator : AbstractValidator<EditUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;

        #endregion

        #region Constructors
        public EditUserValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.FullName)
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(255).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis255]);

            RuleFor(x => x.UserName)
                    .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                    .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                    .MaximumLength(255).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis255]);

            RuleFor(x => x.Email)
                    .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                    .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty]);

        }

        private void ApplyCustomValidationRules()
        {
        }

        #endregion
    }
}
