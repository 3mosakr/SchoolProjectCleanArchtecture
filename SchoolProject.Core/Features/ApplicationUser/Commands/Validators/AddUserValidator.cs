using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;

        #endregion

        #region Constructors
        public AddUserValidator(IStringLocalizer<SharedResources> localizer)
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

            RuleFor(x => x.Password)
                    .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                    .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty]);

            RuleFor(x => x.ConfirmPassword)
                    .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                    .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                    .Equal(x => x.Password).WithMessage(_localizer[SharedResourcesKeys.PasswordNotEqualConfirmPass]);


        }

        private void ApplyCustomValidationRules()
        {
        }

        #endregion
    }
}
