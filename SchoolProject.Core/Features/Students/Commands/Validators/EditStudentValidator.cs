using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validators
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IDepartmentService _departmentService;
        #endregion
        #region Constructors
        public EditStudentValidator(IStudentService studentService, IStringLocalizer<SharedResources> localizer, IDepartmentService departmentService)
        {
            _studentService = studentService;
            _localizer = localizer;
            _departmentService = departmentService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion
        #region Actions
        public void ApplyValidationRules()
        {
            //RuleFor(x => x.NameEn)
            //    .NotEmpty().WithMessage("Name Must not be Empty")
            //    .NotNull().WithMessage("Name Must not be null")
            //    .MaximumLength(50).WithMessage(" Max Length is 50");

            //RuleFor(x => x.Address)
            //    .NotEmpty().WithMessage("{PropertyName} Must not be Empty")
            //    .NotNull().WithMessage("{PropertyName} Must not be null")
            //    .MaximumLength(50).WithMessage("{PropertyValue} Length must be less than 50");
            RuleFor(x => x.NameEn)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                 .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis100]);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis100]);

        }

        public void ApplyCustomValidationRules()
        {
            // check if name is exist
            //RuleFor(x => x.NameEn)
            //    .MustAsync(async (model, Key, CancellationToken) =>
            //    !await _studentService.IsNameExistExcludeSelf(Key, model.Id))
            //    .WithMessage("Name is Exist before");
            RuleFor(x => x.NameEn)
                .MustAsync(async (model, Key, CancellationToken) => !await _studentService.IsNameEnExistExcludeSelf(Key, model.Id))
                .WithMessage(_localizer[SharedResourcesKeys.IsExist]);

            RuleFor(x => x.NameAr)
               .MustAsync(async (model, Key, CancellationToken) => !await _studentService.IsNameArExistExcludeSelf(Key, model.Id))
               .WithMessage(_localizer[SharedResourcesKeys.IsExist]);

            RuleFor(x => x.DepartmentId)
                .MustAsync(async (Key, CancellationToken) => await _departmentService.IsDepartmentIdExist(Key))
                .WithMessage(_localizer[SharedResourcesKeys.IsNotExist]);
        }
        #endregion
    }
}
