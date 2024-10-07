using CourseService.Models.Database;

namespace CourseService.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        public Task<TEntity?> GetByIdAsync(int id);
        public Task<TEntity> InsertAsync(TEntity user);
        public Task<TEntity> UpdateAsync(TEntity user);
        public Task DeleteAsync(TEntity user);
    }
}
