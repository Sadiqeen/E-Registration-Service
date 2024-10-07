using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Models.Dtos
{
    public class AuthRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
