using EventBooking.Application.DTOs;

namespace EventBooking.Application.Services
{
    public interface IUserService
    {
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto> CreateUserAsync(CreateUserDto dto);
        Task<UserDto> UpdateUserAsync(int id, UpdateUserDto dto);
        Task DeleteUserAsync(int id);
    }
}
