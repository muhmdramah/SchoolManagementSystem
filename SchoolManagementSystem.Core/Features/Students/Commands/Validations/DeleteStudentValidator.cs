using FluentValidation;
using SchoolManagementSystem.Core.Features.Students.Commands.Models;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Core.Features.Students.Commands.Validations
{
    public class DeleteStudentValidator : AbstractValidator<DeleteStudentCommand>
    {
        private readonly IStudentService _studentService;

        public DeleteStudentValidator(IStudentService studentService)
        {
            _studentService = studentService;

            ApplyValidationRules();
            CustomValidations();
        }

        private void ApplyValidationRules()
        {
            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("رقم الطالب لازم يكون أكبر من صفر!");
        }

        private void CustomValidations()
        {
            RuleFor(x => x.StudentId)
                .MustAsync(async (studentId, cancellation) =>
                {
                    var student = await _studentService.GetStudentByIdAsync(studentId);
                    return student != null;
                }).WithMessage("الطالب غير موجود!");
        }
    }
}
