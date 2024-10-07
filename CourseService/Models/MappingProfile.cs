using AutoMapper;
using CourseService.Models.Database;
using CourseService.Models.Dtos;

namespace CourseService.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TableCourse, CourseResponseDto>().ReverseMap();
            CreateMap<TableCourse, CourseFormRequestDto>().ReverseMap();
        }
    }
}
