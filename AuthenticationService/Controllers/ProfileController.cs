using System.Threading.Tasks;
using AuthenticationService.Models.Dtos;
using AuthenticationService.Services.Interfaces;
using AuthenticationService.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<UserResponseDto> GetUserProfile()
        {
            UserResponseDto user = ClaimsUtil.GetUserFromToken(User);

            return await _userService.GetUserById(user.Id);
        }
    }
}