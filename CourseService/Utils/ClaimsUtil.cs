
using System.Security.Claims;
using CourseService.Models.Dtos;

namespace CourseService.Utils
{
    public static class ClaimsUtil
    {
        public static UserResponseDto GetUserFromToken(ClaimsPrincipal user)
        {
            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var name = user.FindFirst(ClaimTypes.Name)?.Value;

            // Try to parse userId to an int
            int id = 0;
            if (int.TryParse(idClaim, out var parsedUserId))
            {
                id = parsedUserId;
            }

            return new UserResponseDto
            {
                Id = id,
                Email = email,
                Name = name,
            };
        }
    }
}