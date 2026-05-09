using FluentValidation;
using SchoolManagementSystem.Core.Features.Students.Commands.Models;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Core.Features.Students.Commands.Validations
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentCommand>
    {
        private readonly IStudentService _studentService;

        public CreateStudentValidator(IStudentService studentService)
        {
            ApplyValidationRules();
            CustomValidations();
            _studentService = studentService;
        }

        private void ApplyValidationRules()
        {
            RuleFor(x => x.StudentName)
                .NotEmpty().WithMessage("اسم الطالب لازم يكون موجود!")
                .NotNull().WithMessage("اسم الطالب لازم يكون موجود!")
                .MaximumLength(50).WithMessage("اسم الطالب مينفعش يزيد عن 50 حرف!");

            RuleFor(x => x.StudentAddress)
                .NotEmpty().WithMessage("عنوان الطالب لازم يكون موجود!")
                .MaximumLength(100).WithMessage("عنوان الطالب مينفعش يزيد عن 100 حرف!");

            RuleFor(x => x.StudentPhone)
                .NotEmpty().WithMessage("رقم هاتف الطالب لازم يكون موجود!")
                .MaximumLength(16).WithMessage("رقم هاتف الطالب مينفعش يزيد عن 16 رقم!")
                .Matches(@"^\+201\d{9}$").WithMessage("رقم هاتف الطالب لازم يكون رقم صحيح!");

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("رقم القسم لازم يكون أكبر من صفر!");
        }

        private void CustomValidations()
        {
            RuleFor(x => x.StudentName)
                .MustAsync(async (studentName, cancellation) =>
                {
                    return !await _studentService.IsThisStudentExistAsync(studentName);
                }).WithMessage("الطالب ده موجود بالفعل!");
        }
    }
}
