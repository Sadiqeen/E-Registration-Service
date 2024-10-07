using AuthenticationService.Models.Dtos;

namespace AuthenticationService.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAttempt(AuthRequestDto request);
    }
}
