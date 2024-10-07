using CourseService.Models.Database;
using CourseService.Models.Dtos;

namespace CourseService.Services.Interfaces
{
    public interface ICourseService
    {
        PageResponseDto<TableCourse, CourseResponseDto> GetCourses(PageFilterDto filter);
        Task<CourseResponseDto> GetCourseById(int id);
        Task<CourseResponseDto> CreateNewCourse(CourseFormRequestDto request, int userId);
        Task<CourseResponseDto> UpdateCourse(int id, CourseFormRequestDto request);
        Task DeleteCourse(int id);
    }
}
