using AuthenticationService.Exceptions;
using AuthenticationService.Models.Database;
using AuthenticationService.Models.Dtos;
using AuthenticationService.Options;
using AuthenticationService.Repositories.Interfaces;
using AuthenticationService.Services.Interfaces;
using AuthenticationService.Utils;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationService.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSetting _jwtSetting;
        private readonly IUserRepository _userRepository;

        public AuthService(IOptions<JwtSetting> jwtOptions, IUserRepository userRepository)
        {
            _jwtSetting = jwtOptions.Value;
            _userRepository = userRepository;
        }

        public async Task<AuthResponseDto> LoginAttempt(AuthRequestDto request)
        {
            TableUser? user = await _userRepository.GetUserByEmailAsync(request.Email, null);

            if (user == null)
            {
                throw new NotFoundException($"Invalid username or password.");
            }

            bool validated = PasswordUtils.ValidatePassword(request.Email, request.Password, user.Password);

            if (!validated)
            {
                throw new UnauthorizedException("These credentials do not match our records.");
            }

            return new AuthResponseDto()
            {
                Token = GenerateToken(user),
            };
        }
        public string GenerateToken(TableUser user)
        {
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_jwtSetting.Secret));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            JwtSecurityToken token = new(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
