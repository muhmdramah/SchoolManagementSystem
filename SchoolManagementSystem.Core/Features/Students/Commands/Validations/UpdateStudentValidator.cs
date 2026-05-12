using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolManagementSystem.Core.Features.Students.Commands.Models;
using SchoolManagementSystem.Core.LocalizationResources;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Core.Features.Students.Commands.Validations
{
    public class UpdateStudentValidator : AbstractValidator<UpdateStudentCommand>
    {
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;


        public UpdateStudentValidator(IStudentService studentService,
            IStringLocalizer<SharedResources> stringLocalizer)
        {
            _studentService = studentService;
            _stringLocalizer = stringLocalizer;

            ApplyValidationRules();
            CustomValidations();
        }

        private void ApplyValidationRules()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.StudentNameExistance])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.StudentNameExistance]);

            RuleFor(x => x.StudentName)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.StudentNameExistance])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.StudentNameExistance])
                .MaximumLength(50).WithMessage(_stringLocalizer[SharedResourcesKeys.StudentNameLength]);

            RuleFor(x => x.StudentAddress)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.StudentAddressExistance])
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.StudentAddressLength]);

            RuleFor(x => x.StudentPhone)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.StudentPhoneNumberExistance])
                .MaximumLength(16).WithMessage(_stringLocalizer[SharedResourcesKeys.StudentPhoneNumberLength])
                .Matches(@"^\+201\d{9}$").WithMessage(_stringLocalizer[SharedResourcesKeys.StudentPhoneNumberValidator]);

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage(_stringLocalizer[SharedResourcesKeys.StudentDepartmentNumberMustBeGreaterThanZero]);
        }

        private void CustomValidations()
        {
            RuleFor(x => x.StudentName)
                .MustAsync(async (model, studentName, cancellation) =>
                {
                    return !await _studentService.IsThisStudentExistAsync(studentName, model.StudentId);
                }).WithMessage(_stringLocalizer[SharedResourcesKeys.StudentExistance]);
        }
    }
}
