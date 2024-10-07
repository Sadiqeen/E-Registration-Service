using AuthenticationService.Models.Database;
using AuthenticationService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Repositories
{
    public class UserRepository : BaseRepository<TableUser>, IUserRepository
    {
        public UserRepository(DatabaseContext databaseContext) : base(databaseContext) { }

        public IQueryable<TableUser> GetAsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<TableUser?> GetUserByEmailAsync(string email, int? id)
        {
            IQueryable<TableUser> query = _dbSet.AsQueryable();

            query = query.Where(i => i.Email.ToLower() == email.ToLower());

            if (id.HasValue && id > 0)
            {
                query = query.Where(i => i.Id != id);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
