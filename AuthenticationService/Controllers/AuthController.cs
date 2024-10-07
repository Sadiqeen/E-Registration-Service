using AuthenticationService.Exceptions;
using AuthenticationService.Models.Dtos;
using AuthenticationService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<AuthResponseDto> Login([FromBody] AuthRequestDto request)
        {
            try
            {
                return await _authService.LoginAttempt(request);
            }
            catch (Exception)
            {
                throw new UnauthorizedException("ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง");
            }
        }
    }
}
