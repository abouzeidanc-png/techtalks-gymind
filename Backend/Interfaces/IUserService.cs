
using GYMIND.API.DTOs;
using Microsoft.AspNetCore.Identity.Data;


namespace GYMIND.API.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<GetUserDto>> GetAllUsersAsync();
        Task<GetUserDto?> GetUserByIdAsync(Guid id);
        Task<GetUserDto> CreateUserAsync(CreateUserDto dto);
        Task<bool> UpdateUserAsync(Guid id, UpdateUserDto dto);
        Task<bool> DeactivateUserAsync(Guid id);
        Task<AuthResponseDto?> LoginAsync(LoginRequestDto dto);
        Task<bool> UpdateProfileAsync(Guid userId, EditProfileDto dto);
        Task<AuthResponseDto?> RefreshTokenAsync(string token, string refeshToken);


    }
}
