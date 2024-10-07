using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerifyService.Models.Dtos;
using VerifyService.Services.Interfaces;

namespace VerifyService.Controllers
{
    [Route("api/validate/user")]
    [ApiController]
    public class UserValidatorController : ControllerBase
    {
        private readonly IValidator<UserFormCreateRequestDto> _userFormCreateValidator;
        private readonly IValidator<UserFormUpdateRequestDto> _userFormUpdateValidator;
        private readonly IUserServiceClient _userServiceClient;

        public UserValidatorController(
            IValidator<UserFormCreateRequestDto> userFormCreateValidator,
            IValidator<UserFormUpdateRequestDto> userFormUpdateValidator,
            IUserServiceClient userServiceClient
            )
        {
            _userServiceClient = userServiceClient;
            _userFormCreateValidator = userFormCreateValidator;
            _userFormUpdateValidator = userFormUpdateValidator;
        }

        [HttpPost]
        public async Task<IActionResult> ValidateCreateUserForm([FromBody] UserFormCreateRequestDto form)
        {
            ValidationResult validationResult = await _userFormCreateValidator.ValidateAsync(form);

            bool emailExist = await _userServiceClient.DoesUserExistAsync(form.Email);

            if (emailExist)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("Email", "Email is already in use."));
            }

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> ValidateUpdateUserForm(int id, [FromBody] UserFormUpdateRequestDto form)
        {
            ValidationResult validationResult = await _userFormUpdateValidator.ValidateAsync(form);

            bool emailExist = await _userServiceClient.DoesUserExistAsync(form.Email, id);

            if (emailExist)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("Email", "Email is already in use."));
            }

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            return Ok();
        }
    }
}
