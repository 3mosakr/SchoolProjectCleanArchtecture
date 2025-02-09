using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;

        #endregion

        #region Constructors
        public ChangeUserPasswordValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Id)
                    .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                    .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty]);

            RuleFor(x => x.CurrentPassword)
                    .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                    .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty]);

            RuleFor(x => x.NewPassword)
                    .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                    .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty]);

            RuleFor(x => x.ConfirmPassword)
                    .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                    .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                    .Equal(x => x.NewPassword).WithMessage(_localizer[SharedResourcesKeys.PasswordNotEqualConfirmPass]);


        }

        private void ApplyCustomValidationRules()
        {
        }

        #endregion
    }
}
