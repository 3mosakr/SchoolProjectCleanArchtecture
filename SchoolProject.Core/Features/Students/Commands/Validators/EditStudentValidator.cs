﻿using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validators
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        #endregion
        #region Constructors
        public EditStudentValidator(IStudentService studentService)
        {
            _studentService = studentService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion
        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage("Name Must not be Empty")
                .NotNull().WithMessage("Name Must not be null")
                .MaximumLength(50).WithMessage(" Max Length is 50");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("{PropertyName} Must not be Empty")
                .NotNull().WithMessage("{PropertyName} Must not be null")
                .MaximumLength(50).WithMessage("{PropertyValue} Length must be less than 50");

        }

        public void ApplyCustomValidationRules()
        {
            // check if name is exist
            RuleFor(x => x.NameEn)
                .MustAsync(async (model, Key, CancellationToken) =>
                !await _studentService.IsNameExistExcludeSelf(Key, model.Id))
                .WithMessage("Name is Exist before");
        }
        #endregion
    }
}
