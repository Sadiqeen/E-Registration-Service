using FluentValidation;
using VerifyService.Models.Dtos;

namespace VerifyService.Validator
{
    public class CourseFormValidator : AbstractValidator<CourseFormRequestDto>
    {
        public CourseFormValidator()
        {
            RuleFor(course => course.Name)
                .NotEmpty()
                .WithMessage("Course name is required.")
                .MaximumLength(100)
                .WithMessage("Course name cannot exceed 100 characters.");

            RuleFor(course => course.Description)
                .NotEmpty().WithMessage("Course description is required.")
                .MaximumLength(500).WithMessage("Course description cannot exceed 500 characters.");

            RuleFor(course => course.EnrollmentStart)
                .LessThan(course => course.EnrollmentEnd)
                .WithMessage("Enrollment start must be before enrollment end.");

            RuleFor(course => course.EnrollmentEnd)
                .GreaterThan(course => course.EnrollmentStart)
                .WithMessage("Enrollment end must be after enrollment start.");

            RuleFor(course => course.TeachingStart)
                .LessThan(course => course.TeachingEnd)
                .WithMessage("Teaching start must be before teaching end.");

            RuleFor(course => course.TeachingEnd)
                .GreaterThan(course => course.TeachingStart)
                .WithMessage("Teaching end must be after teaching start.");

            RuleFor(course => course.EnrollmentEnd)
                .LessThan(course => course.TeachingStart)
                .WithMessage("Enrollment end must be before teaching start.");
        }
    }
}