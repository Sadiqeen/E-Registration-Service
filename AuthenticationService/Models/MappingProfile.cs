using AuthenticationService.Models.Database;
using AuthenticationService.Models.Dtos;
using AutoMapper;

namespace AuthenticationService.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TableUser, UserFormCreateRequestDto>().ReverseMap();
            CreateMap<TableUser, UserResponseDto>().ReverseMap();
        }
    }
}
