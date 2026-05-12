using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolManagementSystem.Core.Features.Students.Commands.Models;
using SchoolManagementSystem.Core.LocalizationResources;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Core.Features.Students.Commands.Validations
{
    public class DeleteStudentValidator : AbstractValidator<DeleteStudentCommand>
    {
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public DeleteStudentValidator(IStudentService studentService,
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
                .GreaterThan(0).WithMessage(_stringLocalizer[SharedResourcesKeys.StudentIdGreaterThanZero]);
        }

        private void CustomValidations()
        {
            RuleFor(x => x.StudentId)
                .MustAsync(async (studentId, cancellation) =>
                {
                    var student = await _studentService.GetStudentByIdAsync(studentId);
                    return student != null;
                }).WithMessage(_stringLocalizer[SharedResourcesKeys.StudentExistance]);
        }
    }
}
