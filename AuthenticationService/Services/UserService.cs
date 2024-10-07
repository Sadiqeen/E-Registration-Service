using AuthenticationService.Exceptions;
using AuthenticationService.Models.Database;
using AuthenticationService.Models.Dtos;
using AuthenticationService.Repositories.Interfaces;
using AuthenticationService.Services.Interfaces;
using AuthenticationService.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public PageResponseDto<TableUser, UserResponseDto> GetUsers(PageFilterDto filter)
        {
            IQueryable<TableUser> query = _userRepository.GetAsQueryable();

            if (!string.IsNullOrEmpty(filter.Search))
            {
                query = query.Where(i => i.Name.ToLower().Contains(filter.Search.ToLower())
                    || i.Email.ToLower().Contains(filter.Search.ToLower()));
            }

            return new PageResponseDto<TableUser, UserResponseDto>(_mapper, query, filter.Page, filter.PageSize);
        }

        public async Task<UserResponseDto> GetUserById(int id)
        {
            TableUser? user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException($"The user with ID {id} was not found.");
            }

            return _mapper.Map<TableUser, UserResponseDto>(user);
        }

        public async Task<UserResponseDto> CreateNewUser(UserFormCreateRequestDto request)
        {
            TableUser newUser = _mapper.Map<UserFormCreateRequestDto, TableUser>(request);
            newUser.Password = PasswordUtils.HashPassword(newUser.Email, request.Password);

            try
            {
                await _userRepository.InsertAsync(newUser);
            }
            catch (Exception ex)
            {
                throw new InternalServerException($"An unexpected error occurred while creating a new user: {ex.Message}");
            }

            return _mapper.Map<TableUser, UserResponseDto>(newUser);
        }

        public async Task<UserResponseDto> UpdateUser(int id, UserFormUpdateRequestDto request)
        {
            TableUser? user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException($"The user with ID {id} was not found.");
            }

            user.Name = request.Name;
            user.Email = request.Email;

            try
            {
                await _userRepository.UpdateAsync(user);
            }
            catch (DbUpdateException ex)
            {
                throw new InternalServerException($"Error updating user with ID {id}. Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new InternalServerException($"An unexpected error occurred while updating user with ID {id}: {ex.Message}");
            }

            return _mapper.Map<TableUser, UserResponseDto>(user);
        }

        public async Task DeleteUser(int id)
        {
            TableUser? user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException($"The user with ID {id} was not found.");
            }

            try
            {
                await _userRepository.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                throw new InternalServerException($"An unexpected error occurred while deleting user with ID {id}: {ex.Message}");
            }
        }

        public async Task<UserResponseDto> GetUserByEmail(string email, int? id)
        {
            TableUser? existUser = await _userRepository.GetUserByEmailAsync(email, id);

            if (existUser == null)
            {
                throw new BadRequestException("An account with this email already exists.");
            }

            return _mapper.Map<TableUser, UserResponseDto>(existUser);
        }
    }
}
