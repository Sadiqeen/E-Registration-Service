namespace VerifyService.Models.Dtos
{
    public class UserFormCreateRequestDto : UserFormUpdateRequestDto
    {
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
