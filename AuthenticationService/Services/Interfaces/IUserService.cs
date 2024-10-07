using AuthenticationService.Models.Database;
using AuthenticationService.Models.Dtos;

namespace AuthenticationService.Services.Interfaces
{
    public interface IUserService
    {
        PageResponseDto<TableUser, UserResponseDto> GetUsers(PageFilterDto filter);
        Task<UserResponseDto> GetUserByEmail(string email, int? id);
        Task<UserResponseDto> GetUserById(int id);
        Task<UserResponseDto> CreateNewUser(UserFormCreateRequestDto request);
        Task<UserResponseDto> UpdateUser(int id, UserFormUpdateRequestDto request);
        Task DeleteUser(int id);
    }
}
