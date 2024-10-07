using FluentValidation;
using System.Net;
using VerifyService.Models.Dtos;

namespace VerifyService.Validator
{
    public class UserFormUpdateValidator : AbstractValidator<UserFormUpdateRequestDto>
    {

        public UserFormUpdateValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email address format.");

            RuleFor(user => user.Name)
                .NotEmpty()
                .WithMessage("Name is required.");
        }
    }
}