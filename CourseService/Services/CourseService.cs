using AutoMapper;
using CourseService.Exceptions;
using CourseService.Models.Database;
using CourseService.Models.Dtos;
using CourseService.Repositories.Interfaces;
using CourseService.Services.Interfaces;

namespace CourseService.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;

        public CourseService(IMapper mapper, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        public PageResponseDto<TableCourse, CourseResponseDto> GetCourses(PageFilterDto filter)
        {
            IQueryable<TableCourse> query = _courseRepository.GetAsQueryable();

            if (!string.IsNullOrEmpty(filter.Search))
            {
                query = query.Where(i => i.Name.ToLower().Contains(filter.Search.ToLower())
                    || i.Description.ToLower().Contains(filter.Search.ToLower()));
            }

            return new PageResponseDto<TableCourse, CourseResponseDto>(_mapper, query, filter.Page, filter.PageSize);
        }

        public async Task<CourseResponseDto> GetCourseById(int id)
        {
            TableCourse? course = await _courseRepository.GetByIdAsync(id);

            if (course == null)
            {
                throw new NotFoundException($"The course with ID {id} was not found.");
            }

            return _mapper.Map<TableCourse, CourseResponseDto>(course);
        }

        public async Task<CourseResponseDto> CreateNewCourse(CourseFormRequestDto request, int userId)
        {
            TableCourse newCourse = _mapper.Map<CourseFormRequestDto, TableCourse>(request);
            newCourse.CreatedByUserId = userId;

            try
            {
                await _courseRepository.InsertAsync(newCourse);
            }
            catch (Exception ex)
            {
                throw new InternalServerException($"An unexpected error occurred while creating a new course: {ex.Message}");
            }

            return _mapper.Map<TableCourse, CourseResponseDto>(newCourse);
        }

        public async Task<CourseResponseDto> UpdateCourse(int id, CourseFormRequestDto request)
        {
            TableCourse? course = await _courseRepository.GetByIdAsync(id);

            if (course == null)
            {
                throw new NotFoundException($"The course with ID {id} was not found.");
            }

            course.Name = request.Name;
            course.Description = request.Description;
            course.EnrollmentStart = request.EnrollmentStart;
            course.EnrollmentEnd = request.EnrollmentEnd;
            course.TeachingStart = request.TeachingStart;
            course.TeachingEnd = request.TeachingEnd;

            try
            {
                await _courseRepository.UpdateAsync(course);
            }
            catch (Exception ex)
            {
                throw new InternalServerException($"An unexpected error occurred while updating course with ID {id}: {ex.Message}");
            }

            return _mapper.Map<TableCourse, CourseResponseDto>(course);
        }

        public async Task DeleteCourse(int id)
        {
            TableCourse? course = await _courseRepository.GetByIdAsync(id);

            if (course == null)
            {
                throw new NotFoundException($"The course with ID {id} was not found.");
            }

            try
            {
                await _courseRepository.DeleteAsync(course);
            }
            catch (Exception ex)
            {
                throw new InternalServerException($"An unexpected error occurred while deleting course with ID {id}: {ex.Message}");
            }
        }
    }
}
