namespace VerifyService.Services.Interfaces
{
    public interface IUserServiceClient
    {
        Task<bool> DoesUserExistAsync(string email, int? id = null);
    }
}
