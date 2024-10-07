using CourseService.Models.Database;
using CourseService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Repositories
{
    public class CourseRepository : BaseRepository<TableCourse>, ICourseRepository
    {
        public CourseRepository(DatabaseContext databaseContext) : base(databaseContext) { }

        public IQueryable<TableCourse> GetAsQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}
