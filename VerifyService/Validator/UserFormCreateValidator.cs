using FluentValidation;
using System.Net;
using VerifyService.Models.Dtos;
using VerifyService.Services.Interfaces;

namespace VerifyService.Validator
{
    public class UserFormCreateValidator : AbstractValidator<UserFormCreateRequestDto>
    {

        public UserFormCreateValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email address format.");

            RuleFor(user => user.Name)
                .NotEmpty()
                .WithMessage("Name is required.");

            RuleFor(user => user.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long.");

            RuleFor(user => user.PasswordConfirmation)
                .NotEmpty()
                .WithMessage("Password confirmation is required.")
                .Equal(user => user.Password)
                .WithMessage("The password and confirmation password do not match.");
        }
    }
}
