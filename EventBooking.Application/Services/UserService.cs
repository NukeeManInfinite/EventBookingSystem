using EventBooking.Application.DTOs;
using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;

namespace EventBooking.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return MapToDto(user);
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return null;

            return MapToDto(user);
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(MapToDto).ToList();
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new Exception("User with this email already exists");

            var user = new User
            {
                Email = dto.Email,
                Name = dto.Name
            };

            var created = await _userRepository.AddAsync(user);
            return MapToDto(created);
        }

        public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("User not found");

            user.Email = dto.Email;
            user.Name = dto.Name;

            await _userRepository.UpdateAsync(user);
            return MapToDto(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }
    }
}
