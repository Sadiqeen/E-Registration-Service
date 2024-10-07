using CourseService.Exceptions;
using CourseService.Models.Database;
using CourseService.Models.Dtos;
using CourseService.Services.Interfaces;
using CourseService.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/course")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IConfiguration _configuration;

        public CourseController(ICourseService courseService, IConfiguration configuration)
        {
            _courseService = courseService;
            _configuration = configuration;
        }

        [HttpGet]
        public PageResponseDto<TableCourse, CourseResponseDto> GetCourses([FromQuery] PageFilterDto filter)
        {
            return _courseService.GetCourses(filter);
        }

        [HttpGet("{id}")]
        public async Task<CourseResponseDto> GetCourseById(int id)
        {
            return await _courseService.GetCourseById(id);
        }

        [HttpPost]
        public async Task<CourseResponseDto> CreateCourse([FromBody] CourseFormRequestDto request)
        {
            string errorMessage = await Validator.ValidateAsync<CourseFormRequestDto>(_configuration, "/api/validate/course", request);

            if (errorMessage != null)
            {
                throw new BadRequestException(errorMessage);
            }
            
            UserResponseDto user = ClaimsUtil.GetUserFromToken(User);

            return await _courseService.CreateNewCourse(request, user.Id);
        }

        [HttpPut("{id}")]
        public async Task<CourseResponseDto> UpdateCourse(int id, [FromBody] CourseFormRequestDto request)
        {
            string errorMessage = await Validator.ValidateAsync<CourseFormRequestDto>(_configuration, "/api/validate/course", request);

            if (errorMessage != null)
            {
                throw new BadRequestException(errorMessage);
            }

            return await _courseService.UpdateCourse(id, request);
        }

        [HttpDelete("{id}")]
        public async Task<NoContentResult> DeleteCourse(int id)
        {
            await _courseService.DeleteCourse(id);
            return NoContent();
        }

        /* -------------------------------------------------------------------------- */
        /*                               For Service API                              */
        /* -------------------------------------------------------------------------- */

        [HttpGet("api-key/{id}")]
        [AllowAnonymous]
        [ApiKeyAuthorize]
        public async Task<CourseResponseDto> GetCourseByIdViaApi(int id)
        {
            return await _courseService.GetCourseById(id);
        }
    }
}