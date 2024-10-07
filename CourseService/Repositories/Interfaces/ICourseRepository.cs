using CourseService.Models.Database;

namespace CourseService.Repositories.Interfaces
{
    public interface ICourseRepository : IBaseRepository<TableCourse>
    {
        public IQueryable<TableCourse> GetAsQueryable();
    }
}
