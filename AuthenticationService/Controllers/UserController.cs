using AuthenticationService.Exceptions;
using AuthenticationService.Models.Database;
using AuthenticationService.Models.Dtos;
using AuthenticationService.Services.Interfaces;
using AuthenticationService.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        public PageResponseDto<TableUser, UserResponseDto> GetUsers([FromQuery] PageFilterDto filter)
        {
            return _userService.GetUsers(filter);
        }

        [HttpGet("{id}")]
        public async Task<UserResponseDto> GetUserById(int id)
        {
            return await _userService.GetUserById(id);
        }

        [HttpPost]
        public async Task<UserResponseDto> CreateUser([FromBody] UserFormCreateRequestDto request)
        {
            string errorMessage = await Validator.ValidateAsync<UserFormCreateRequestDto>(_configuration, "/api/validate/user", request);

            if (errorMessage != null)
            {
                throw new BadRequestException(errorMessage);
            }

            return await _userService.CreateNewUser(request);
        }

        [HttpPut("{id}")]
        public async Task<UserResponseDto> UpdateUser(int id, [FromBody] UserFormUpdateRequestDto request)
        {
            string errorMessage = await Validator.ValidateAsync<UserFormUpdateRequestDto>(_configuration, $"/api/validate/user/{id}", request);

            if (errorMessage != null)
            {
                throw new BadRequestException(errorMessage);
            }

            return await _userService.UpdateUser(id, request);
        }

        [HttpDelete("{id}")]
        public async Task<NoContentResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }

        /* -------------------------------------------------------------------------- */
        /*                               For Service API                              */
        /* -------------------------------------------------------------------------- */

        [HttpGet("api-key/exist")]
        [AllowAnonymous]
        [ApiKeyAuthorize]
        public async Task<bool> GetCourseByIdWithApi([FromQuery] string email, [FromQuery] int id)
        {
            try
            {
                await _userService.GetUserByEmail(email, id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
