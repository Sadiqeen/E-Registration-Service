using AuthenticationService.Models.Database;

namespace AuthenticationService.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<TableUser>
    {
        IQueryable<TableUser> GetAsQueryable();
        Task<TableUser?> GetUserByEmailAsync(string email, int? id);
    }
}
