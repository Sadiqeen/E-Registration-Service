using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using VerifyService.Models.Dtos;

namespace VerifyService.Controllers
{
    [Route("api/validate/course")]
    [ApiController]
    public class CourseValidatorController : ControllerBase
    {
        private readonly IValidator<CourseFormRequestDto> _courseFormValidator;

        public CourseValidatorController(IValidator<CourseFormRequestDto> courseFormValidator)
        {
            _courseFormValidator = courseFormValidator;
        }

        [HttpPost]
        public async Task<IActionResult> ValidateCourseForm([FromBody] CourseFormRequestDto form)
        {
            ValidationResult validationResult = await _courseFormValidator.ValidateAsync(form);

             if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            return Ok();
        }
    }
}