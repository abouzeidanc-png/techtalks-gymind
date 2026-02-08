
using GYMIND.API.DTOs;
using Microsoft.AspNetCore.Identity.Data;


namespace GYMIND.API.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto?> GetUserByIdAsync(Guid id);
        Task<UserResponseDto> CreateUserAsync(CreateUserDto dto);
        Task<bool> UpdateUserAsync(Guid id, UpdateUserDto dto);
        Task<bool> DeactivateUserAsync(Guid id);
        Task<string?> LoginAsync(LoginRequestDto dto);

    }
}
