using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Models.Dtos
{
    public class UserFormCreateRequestDto : UserFormUpdateRequestDto
    {
        public string Password { get; set; } = string.Empty;

        public string PasswordConfirmation { get; set; } = string.Empty;
    }
}
