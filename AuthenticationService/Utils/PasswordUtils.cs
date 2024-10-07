using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Utils
{
    public static class PasswordUtils
    {

        public static bool ValidatePassword(string email, string enteredPassword, string storedHashedPassword)
        {
            PasswordHasher<IdentityUser> passwordHasher = new();

            IdentityUser user = new(email);
            PasswordVerificationResult verificationResult = passwordHasher
                    .VerifyHashedPassword(user, storedHashedPassword, enteredPassword);

            return verificationResult == PasswordVerificationResult.Success;
        }

        public static string HashPassword(string email, string password)
        {
            PasswordHasher<IdentityUser> passwordHasher = new();

            IdentityUser user = new(email);
            string hashedPassword = passwordHasher.HashPassword(user, password);
            return hashedPassword;
        }
    }
}
