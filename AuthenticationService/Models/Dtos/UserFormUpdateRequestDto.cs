using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Models.Dtos
{
    public class UserFormUpdateRequestDto
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
